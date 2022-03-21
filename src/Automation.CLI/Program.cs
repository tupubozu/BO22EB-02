using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Configuration;
using Automation.Cryptography;
using Renci.SshNet;
using Serilog;
using System.IO;
using Automation.Core;

namespace Automation.CLI
{
    internal static class Program
    {
        static ProgramConfiguration config;
        static readonly Dictionary<ushort, byte[]> ScriptDict = new Dictionary<ushort, byte[]>();
        static readonly Dictionary<ushort, byte[]> KeyDict = new Dictionary<ushort,byte[]>();

        static async Task Main(string[] args)
        {
            InitializeLog();
            Log.Information("Starting program");

            if (args.Length == 0)
            {
                Log.Fatal("No arguments provided");
                return;
            }

            Log.Information("Configuration argument: \"{0}\"", args[0]);
            if (!File.Exists(args[0]))
            {
                Log.Fatal("No file exist at \"{0}\"", args[0]);
                return;
            }
            
            try
            {
                string configArchivePath = Path.GetFullPath(args[0]);
                Log.Information("Reading configuration from: \"{0}\"", configArchivePath);

                Console.Write("Password: ");
                var password = Console.ReadLine();

                using (var fileStream = File.OpenRead(configArchivePath))
                    await ConfigCore.OpenConfigAsync(fileStream, password, config, ScriptDict, KeyDict);

                Log.Information("Read configuration \"{0}\" made by {1} ({2}) on {3:O}", config.Metadata.Title, config.Metadata.Author.Name, config.Metadata.Author.Email,config.Metadata.Revision);
                Console.WriteLine("Description: {0}",config.Metadata.Description);
            }
            catch (Exception ex)
            {
                Log.Debug("Exception: {0}", ex);
                Log.Fatal("Error encountered while reading configuration: {0}: {1}", ex.GetType().FullName, ex.Message);
                return;
            }

            if (config.Work is null)
            {
                Log.Error("No targets provided in configuration");
                return;
            }

            if (config.Keys is null || config.Keys.Length == 0)
                Log.Warning("No keys provided in configuration");

            if (config.Scripts is null || config.Scripts.Length == 0)
                Log.Information("No scripts provided in configuration");

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
                .WriteTo.Console(Serilog.Events.LogEventLevel.Fatal)
#endif
                .Enrich.FromLogContext()
                .MinimumLevel.Debug();

            Log.Logger = logConfig.CreateLogger();
        }
    }
}
