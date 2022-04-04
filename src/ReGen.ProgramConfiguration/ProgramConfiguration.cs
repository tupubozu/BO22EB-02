using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace ReGen.Configuration
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "ProgramConfiguration")]
    public partial class ProgramConfiguration
    {
        [XmlIgnore()]
        public Dictionary<ushort, byte[]> ScriptBytes { get; set; }
        
        [XmlIgnore()]
        public Dictionary<ushort, byte[]> KeyBytes { get; set; }

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
