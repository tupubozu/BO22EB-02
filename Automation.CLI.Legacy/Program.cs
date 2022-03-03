using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Automation.ProgramConfiguration;

namespace Automation.CLI.Legacy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var paths = args.Where((string a) => System.IO.File.Exists(a));
            foreach (var path in paths)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                    var config = serializer.Deserialize(System.IO.File.OpenRead(path)) as Configuration;
                }
                catch(Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex);
                }
            }
        }
    }
}
