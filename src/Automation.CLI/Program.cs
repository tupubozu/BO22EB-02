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

            var results = await config.Work.Execute();

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

        static async Task<JobResult[]> Execute (this ProgramConfiguration.Target[] Work)
        {
            List<JobResult> results = new List<JobResult>();

            foreach (var target in Work)
            {
                var categories = target.Jobs.Select((item) => item.Category);
                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.Custom))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.Custom);
                    foreach (var job in jobs)
                    {
                        using (var client = new SshClient(
                            host: target.Host,
                            username: config.Keys
                                .Where(item => job.Keys.Contains(item.ID))
                                .Select(key => key.User)
                                .First(),
                            port: (int)job.Port,
                            keyFiles: KeyDict
                                .Where(item => job.Keys.Contains(item.Key))
                                .Select(key => new PrivateKeyFile(new MemoryStream(key.Value)))
                                .ToArray()
                                ))
                        {
                            var commands = ScriptDict
                                .Where(item => job.Scripts.Contains(item.Key))
                                .Select(item => (client.CreateCommand(Encoding.UTF8.GetString(item.Value)), item.Key)).ToArray();

                            var commandOutput = commands.Select(async (item) => await Task.Run(() => item.Item1.Execute()));
                            var res = commands.Select((item) => item.Item1.ExitStatus).ToArray();
                            commands.Select((item) => { item.Item1.Dispose(); return true; });
                            
                        }
                    }
                }
                
                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.McAfee))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.McAfee);

                }

                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.Acronis))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.Acronis);

                }

                if (categories.Contains(ProgramConfiguration.Target.Job.JobCategory.vCenter))
                {
                    var jobs = target.Jobs.Where(item => item.Category == ProgramConfiguration.Target.Job.JobCategory.vCenter);

                }
            }

            throw new NotImplementedException();
        }
    }
    internal class JobResult
    {
        public JobResult[] InternalResults;

        public string Message;

    }
}
