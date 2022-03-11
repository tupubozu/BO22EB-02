using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Automation.Configuration;
using Renci.SshNet;
using Serilog;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Automation.CLI.Legacy
{
    internal static class Program
    {
        static ProgramConfiguration config;
        static void Main(string[] args)
        {
            InitializeLog();
            Log.Information("Starting program");

            if (args.Length == 0)
            {
                Log.Fatal("No arguments provided");
                Log.Information("Exiting");
#if DEBUG
                Console.ReadKey(true);
#endif
                return;
            }

            Log.Information("Configuration argument: \"{0}\"", args[0]);
            if (!File.Exists(args[0]))
            {
                Log.Fatal("No file exist at \"{0}\"", args[0]);
                Log.Information("Exiting");
                return;
            }
            string configArchivePath = Path.GetFullPath(args[0]);
            Log.Information("Reading configuration from: \"{0}\"", configArchivePath);

            try
            {
                using (var configArchiveStream = File.Open(configArchivePath, FileMode.Open, FileAccess.Read))
                using (var zipConfig = new ZipArchive(configArchiveStream, ZipArchiveMode.Read, true, Encoding.UTF8))
                using (var hash = SHA512.Create())
                using (var aesCrypto = Aes.Create())
                {
                    aesCrypto.BlockSize = 512;
                    aesCrypto.KeySize = 512;
                    aesCrypto.Mode = CipherMode.CBC;
                    aesCrypto.Padding = PaddingMode.ISO10126;
                    aesCrypto.IV = hash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes("This is a completely valid vay of initializing the IV. Trust me, I am an engineer!")));
                    aesCrypto.Key = hash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(Console.ReadLine())));

                    var encryptedConfig = zipConfig.GetEntry("config.xml.aes");

                    using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        if (!ConfigurationTryParse(cryptoConfigStream, out config, out Exception ex))
                        {
                            throw ex;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Debug("Exception: {0}", ex);
                Log.Error("Exception: {0}", ex.Message);
                Log.Fatal("Error encountered while reading configuration: {0}: {1}", ex.GetType().FullName, ex.Message);
                Log.Information("Exiting");
                return;
            }

            Console.WriteLine(config.Metadata.Description);

            var a = config.Work.Last();
            var b = a.Job.First();

            var sshClient = new SshClient(host: a.Host, port: b.Port, username: b.Username, keyFiles: new PrivateKeyFile(new MemoryStream(Encoding.Unicode.GetBytes(b.Key))));
            Console.WriteLine(sshClient.ConnectionInfo);
#if DEBUG
            Console.ReadKey(true);
#endif
        }
        static bool ConfigurationTryParse(Stream stream, out ProgramConfiguration config, out Exception ex)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProgramConfiguration));
                config = serializer.Deserialize(stream) as ProgramConfiguration;
                ex = null;
                return true;
            }
            catch (Exception error)
            {
                config = null;
                ex = error;
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
