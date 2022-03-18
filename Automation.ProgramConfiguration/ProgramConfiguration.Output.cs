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
        public class Output
        {
            [XmlAttribute("category")]
            public OutputCategory Category { get; set; }

            [XmlAttribute("address")]
            public string Address { get; set; }

            [XmlAttribute("password")]
            public bool Password { get; set; }

            public enum OutputCategory { File, Email, Console }
        }
    }
}
