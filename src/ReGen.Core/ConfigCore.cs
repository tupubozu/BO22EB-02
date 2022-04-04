using ReGen.Configuration;
using ReGen.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReGen.Core
{
    public static class ConfigCore
    {
        private static readonly string InitVectText = "I'm an engineer!";
        private static readonly string XmlConfigZipEntryName = "config.xml.aes";

        public static async Task SaveConfigAsync(Stream stream, string password, ProgramConfiguration config)
        {
            using (var zipConfig = new ZipArchive(stream, ZipArchiveMode.Create, true, Encoding.UTF8))
            {
                if (!(config.Keys is null))
                    foreach (var key in config.Keys)
                    {
                        if (key.Category == ProgramConfiguration.Key.KeyCategory.SSH)
                            using (var aesCrypto = Crypto.GetAes())
                            {
                                key.EncryptionIV = aesCrypto.IV;
                                key.EncryptionKey = aesCrypto.Key;

                                key.Source = $"k#{key.ID}.aes";

                                var encryptedConfig = zipConfig.CreateEntry(key.Source);

                                using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateEncryptor(), CryptoStreamMode.Write))
                                using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8))
                                {
                                    await writer.WriteAsync(Encoding.UTF8.GetString(config.KeyBytes[key.ID]));
                                    await writer.FlushAsync();
                                }
                            }
                    }

                if (!(config.Scripts is null))
                    foreach (var script in config.Scripts)
                    {
                        using (var aesCrypto = Crypto.GetAes())
                        {
                            script.EncryptionIV = aesCrypto.IV;
                            script.EncryptionKey = aesCrypto.Key;

                            script.Source = $"s#{script.ID}.aes";

                            var encryptedConfig = zipConfig.CreateEntry(script.Source);

                            using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateEncryptor(), CryptoStreamMode.Write))
                            using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8))
                            {
                                await writer.WriteAsync(Encoding.UTF8.GetString(config.ScriptBytes[script.ID]));
                                await writer.FlushAsync();
                            }
                        }
                    }

                using (var aesCrypto = Crypto.GetAes(Encoding.UTF8.GetBytes(InitVectText)))
                using (var passwordHash = SHA256.Create())
                {
                    aesCrypto.Key = passwordHash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(password)));

                    var encryptedConfig = zipConfig.CreateEntry(XmlConfigZipEntryName, CompressionLevel.Optimal);

                    using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoConfigStream, Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ProgramConfiguration));
                        config.Metadata.Revision = DateTime.Now;
                        serializer.Serialize(writer, config);
                    }

                }

            }
        }

        public static async Task<ProgramConfiguration> OpenConfigAsync(Stream stream, string password)
        {
            ProgramConfiguration config = new ProgramConfiguration
            {
                ScriptBytes = new Dictionary<ushort, byte[]>(),
                KeyBytes = new Dictionary<ushort, byte[]>()
            };

            using (var zipConfig = new ZipArchive(stream, ZipArchiveMode.Read, true, Encoding.UTF8))
            {
                using (var aesCrypto = Crypto.GetAes(Encoding.UTF8.GetBytes(InitVectText)))
                using (var passwordHash = SHA256.Create())
                {
                    aesCrypto.Key = passwordHash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(password)));

                    var encryptedConfig = zipConfig.GetEntry(XmlConfigZipEntryName);

                    using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                    {
                        config = ProgramConfiguration.Parse(new MemoryStream(Encoding.UTF8.GetBytes(await reader.ReadToEndAsync())));
                    }
                }

                if (!(config.Keys is null))
                    foreach (var key in config.Keys)
                    {
                        switch (key.Category)
                        {
                            case ProgramConfiguration.Key.KeyCategory.API:
                                config.KeyBytes.Add(key.ID, Encoding.UTF8.GetBytes(key.Value));
                                break;
                            case ProgramConfiguration.Key.KeyCategory.SSH:
                                using (var aesCrypto = Crypto.GetAes())
                                {
                                    aesCrypto.IV = key.EncryptionIV;
                                    aesCrypto.Key = key.EncryptionKey;

                                    var encryptedConfig = zipConfig.GetEntry(key.Source);

                                    using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                                    using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                                    {
                                        config.KeyBytes.Add(key.ID, Encoding.UTF8.GetBytes(await reader.ReadToEndAsync()));
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                if (!(config.Scripts is null))
                    foreach (var script in config.Scripts)
                    {
                        using (var aesCrypto = Crypto.GetAes())
                        {
                            aesCrypto.IV = script.EncryptionIV;
                            aesCrypto.Key = script.EncryptionKey;

                            var encryptedConfig = zipConfig.GetEntry(script.Source);

                            using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                            using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                            {
                                config.ScriptBytes.Add(script.ID, Encoding.UTF8.GetBytes(await reader.ReadToEndAsync()));
                            }
                        }
                    }

                return config;
            }
        }
    }
}
