using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace Automation.Configuration
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "ProgramConfiguration")]
    public partial class ProgramConfiguration
    {
        public ConfigurationMetadata Metadata { get; set; }

        [XmlArrayItem("Output", IsNullable = false)]
        public Output[] Options { get; set; }

        [XmlArrayItem("Target", IsNullable = false)]
        public Target[] Work { get; set; }

        [XmlArrayItem("Key", IsNullable = false)]
        public Key[] Keys { get; set; }

        [XmlArrayItem("Script", IsNullable = false)]
        public Script[] Scripts { get; set; }

        public static bool TryParse(Stream stream, out ProgramConfiguration config)
        {
            try
            {
                config = Parse(stream);
                return true;
            }
            catch (System.Exception)
            {
                config = null;
                return false;
            }
        }

        public static ProgramConfiguration Parse(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramConfiguration));
            return serializer.Deserialize(stream) as ProgramConfiguration;
        }

        public override string ToString()
        {
            return Metadata is null ? base.ToString() : $"Configuration {Metadata.Title}";
        }
    }
}
