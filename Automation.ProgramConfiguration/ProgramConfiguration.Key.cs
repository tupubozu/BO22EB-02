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
        public class Key
        {
            [XmlAttribute("id")]
            public byte ID { get; set; }

            [XmlAttribute("name")]
            public string Name { get; set; }

            [XmlAttribute("category")]
            public KeyCategory Category { get; set; }

            [XmlAttribute("src")]
            public string Source { get; set; }

            [XmlAttribute("user")]
            public string User { get; set; }

            [XmlText()]
            public string Value { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
            }

            public enum KeyCategory { API, SSH}
        }
    }
}
