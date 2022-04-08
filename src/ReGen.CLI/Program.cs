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

                foreach (var reportName in config.Options.Where(i => i.Category == ProgramConfiguration.Output.OutputCategory.File).Select(i => i.Address))
                    using (var baseStream = File.Open(reportName, FileMode.Create, FileAccess.ReadWrite))
                    using (var reportFile = SpreadsheetDocument.Create(baseStream, SpreadsheetDocumentType.Workbook))
                    {
                        Log.Information("Creating report: {0}", reportName);
                        var wbPart = reportFile.AddWorkbookPart();
                        wbPart.Workbook = new Workbook();

                        var wsPart = wbPart.AddNewPart<WorksheetPart>();
                        //wsPart.Worksheet = new Worksheet(new SheetData());

                        var sheets = reportFile.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                        var sheet = new Sheet()
                        {
                            Id = reportFile.WorkbookPart.GetIdOfPart(wsPart),
                            SheetId = 1,
                            Name = "Dummy"
                        };
                        sheets.Append(sheet);

                        Worksheet worksheet = new Worksheet();
                        SheetData sheetData = new SheetData();
                        Row row = new Row();
                        Cell cell = new Cell()
                        {
                            CellReference = "A1",
                            DataType = CellValues.String,
                            CellValue = new CellValue("Microsoft")
                        };
                        row.Append(cell);

                        cell = new Cell()
                        {
                            CellReference = "B1",
                            DataType = CellValues.String,
                            CellValue = new CellValue("Microsoft")
                        };
                        row.Append(cell);
                        sheetData.Append(row);
                        worksheet.Append(sheetData);

                        wsPart.Worksheet = worksheet;

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
