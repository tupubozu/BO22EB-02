using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Automation.ProgramConfiguration;
using Renci.SshNet;

namespace Automation.CLI.Legacy
{
    internal static class Program
    {
        static Configuration config;
        static void Main(string[] args)
        {
            var paths = args.Where((string path) => System.IO.File.Exists(path));
            foreach (var path in paths)
            {
                ConfigurationTryParse(System.IO.File.OpenRead(path), out config);
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
                Console.Out.WriteLine(ex.Message);
                Console.Error.WriteLine(ex);
#endif
                config = null;
                return false;
            }
            
        }
    }
}
