using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Automation.Configuration;
using Automation.Cryptography;
using Renci.SshNet;
using Serilog;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Automation.CLI
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
                using (var aesCrypto = Crypto.GetAes(Encoding.UTF8.GetBytes("I'm an engineer!")))
                using (var passwordHash = SHA256.Create())
                {
                    Console.Write("Password: ");
                    aesCrypto.Key = passwordHash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(Console.ReadLine())));

                    var encryptedConfig = zipConfig.GetEntry("config.xml.aes");

                    using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        config = ProgramConfiguration.Parse(cryptoConfigStream);
                    }
                }

                Log.Information("Read configuration \"{0}\" made by {1} ({2}) on {3:O}", config.Metadata.Title, config.Metadata.Author.Name, config.Metadata.Author.Email,config.Metadata.Revision);
                Console.WriteLine("Description: {0}",config.Metadata.Description);
            }
            catch (Exception ex)
            {
                Log.Debug("Exception: {0}", ex);
                Log.Error("Exception: {0}", ex.Message);
                Log.Fatal("Error encountered while reading configuration: {0}: {1}", ex.GetType().FullName, ex.Message);
                return;
            }

            Log.Information("Exiting");
#if DEBUG
            Console.ReadKey(true);
#endif
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
