using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Automation.Configuration
{
    public partial class ProgramConfiguration
    {
        [Serializable()]
        [DesignerCategory("code")]
        [XmlType(AnonymousType = true)]
        public class Script
        {
            [XmlAttribute("id")]
            public ushort ID { get; set; }

            [XmlAttribute("name")]
            public string Name { get; set; }

            [XmlAttribute("src")]
            public string Source { get; set; }

            [XmlAttribute("key")]
            public string Key { get; set; }

            [XmlAttribute("iv")]
            public string IV { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
            }
        }
    }
}
