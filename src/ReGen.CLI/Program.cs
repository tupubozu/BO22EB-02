using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReGen.Configuration;
using ReGen.Cryptography;
using Renci.SshNet;
using Renci.SshNet.Common;
using Serilog;
using System.IO;
using ReGen.Core;

namespace ReGen.CLI
{
    internal static class Program
    {
        static ProgramConfiguration config;
        
        static async Task Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            var assemblyName = assembly.GetName();
            Console.WriteLine("{0} v{1} {2} {3}", assemblyName.Name, assemblyName.Version, assemblyName.ProcessorArchitecture, Environment.NewLine);
            
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
                    config = await ConfigCore.OpenConfigAsync(fileStream, password);

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

            try
            {
                var results = config.Execute();
                //Task.WaitAll(results);

                foreach (var reportName in config.Options.Where(i => i.Category == ProgramConfiguration.Output.OutputCategory.File).Select(i => i.Address))
                    using (var baseStream = File.Open(reportName, FileMode.Create, FileAccess.ReadWrite))
                    using (var reportFile = SpreadsheetDocument.Create(baseStream, SpreadsheetDocumentType.Workbook))
                    {
                        Log.Information("Creating report: {0}", reportName);
                        var wbPart = reportFile.AddWorkbookPart();
                        wbPart.Workbook = new Workbook();

                        for (uint i = 0; i < results.Length; i++)
                        {
                            var result = await results[i];

                            var wsPart = wbPart.AddNewPart<WorksheetPart>();
                            
                            var sheets = reportFile.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                            var sheet = new Sheet()
                            {
                                Id = reportFile.WorkbookPart.GetIdOfPart(wsPart),
                                SheetId = i + 1,
                                Name = $"{result.Target}; {result.JobName}"
                            };
                            sheets.Append(sheet);

                            wsPart.Worksheet = result.Worksheet;
                        }
                    }
            }
            catch (Exception ex)
            {
                Log.Debug("Exception: {0}", ex);
                Log.Error("Error encountered: {0}: {1}", ex.GetType().FullName, ex.Message);
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
                //.WriteTo.Console(Serilog.Events.LogEventLevel.Warning)
                //.WriteTo.Console(Serilog.Events.LogEventLevel.Error)
                //.WriteTo.Console(Serilog.Events.LogEventLevel.Fatal)
#endif
                .Enrich.FromLogContext()
                .MinimumLevel.Debug();

            Log.Logger = logConfig.CreateLogger();
        }
    }
}
