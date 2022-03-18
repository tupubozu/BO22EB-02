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
        public partial class Target
        {
            [XmlElement("Job")]
            public Job[] Jobs { get; set; }

            [XmlAttribute("host")]
            public string Host { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Host) ? base.ToString() : Host;
            }
        }
    }
}
