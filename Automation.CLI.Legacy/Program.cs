using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Automation.ProgramConfiguration;
using Renci.SshNet;
<<<<<<< HEAD
using Serilog;
=======
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
>>>>>>> dev-crypto

namespace Automation.CLI.Legacy
{
    internal static class Program
    {
        static Configuration config;
        static void Main(string[] args)
        {
<<<<<<< HEAD
            InitializeLog();
            if (!ConfigurationTryParse(System.IO.File.OpenRead(args[0]), out config))
            {
                return;
=======
            if (args.Length == 0) return;
            string configArchivePath = args[0];

            using (var configArchiveStream = File.Open(configArchivePath, FileMode.Open, FileAccess.Read))
            using (var zipConfig = new ZipArchive(configArchiveStream, ZipArchiveMode.Read, true, Encoding.UTF8))
            using (var hash = SHA512.Create())
            using (var aesCrypto = Aes.Create())
            {
                aesCrypto.BlockSize = 512;
                aesCrypto.KeySize = 512;
                aesCrypto.Mode = CipherMode.CBC;
                aesCrypto.Padding = PaddingMode.ISO10126;
                aesCrypto.IV = hash.ComputeHash(new MemoryStream(Encoding.Unicode.GetBytes("This is a completely valid vay of initializing the IV. Trust me, I am an engineer!")));
                aesCrypto.Key = hash.ComputeHash(new MemoryStream(Encoding.Unicode.GetBytes(Console.ReadLine())));

                var encryptedConfig = zipConfig.GetEntry("config.xml.aes");

                using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    if (!ConfigurationTryParse(cryptoConfigStream, out config)) return;
                }
                
>>>>>>> dev-crypto
            }
            

            var a = config.Work.Last();
            var b = a.Task.First();

            var sshClient = new SshClient(host: a.Host, port: b.Port, username: b.Username, keyFiles: new PrivateKeyFile(b.Key) );
            Console.WriteLine(sshClient.ConnectionInfo);
#if DEBUG
            Console.ReadKey(true);
#endif
        }
        static bool ConfigurationTryParse(System.IO.Stream stream, out Automation.ProgramConfiguration.Configuration config)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                config = serializer.Deserialize(stream) as Configuration;
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                Log.Information(ex.Message);
                Log.Debug("Error: {0}",ex);
#endif
                config = null;
                return false;
            }
            
        }
        static void InitializeLog()
        {
            var logConfig = new LoggerConfiguration()
#if DEBUG
                .WriteTo.Console(Serilog.Events.LogEventLevel.Verbose)
#else
				.WriteTo.Console(Serilog.Events.LogEventLevel.Information)
				.WriteTo.Console(Serilog.Events.LogEventLevel.Warning)
				.WriteTo.Console(Serilog.Events.LogEventLevel.Error)
#endif
                .Enrich.FromLogContext()
                .MinimumLevel.Debug();

            Log.Logger = logConfig.CreateLogger();
        }
    }
}
