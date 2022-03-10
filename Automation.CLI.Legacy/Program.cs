using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Automation.ProgramConfiguration;
using Renci.SshNet;
using Serilog;

namespace Automation.CLI.Legacy
{
    internal static class Program
    {
        static Configuration config;
        static void Main(string[] args)
        {
            InitializeLog();
            if (!ConfigurationTryParse(System.IO.File.OpenRead(args[0]), out config))
            {
                return;
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
