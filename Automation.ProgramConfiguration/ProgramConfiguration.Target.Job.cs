using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Automation.Configuration
{
    public partial class ProgramConfiguration
    {
        public partial class Target
        {
            [Serializable()]
            [DesignerCategory("code")]
            [XmlType(AnonymousType = true)]
            public class Job
            {
                [XmlAttribute("name")]
                public string Name { get; set; }

                [XmlAttribute("category")]
                public JobCategory Category { get; set; }

                [XmlAttribute("port")]
                public ushort Port { get; set; }

                [XmlAttribute("keys")]
                public byte[] Keys { get; set; }

                [XmlAttribute("scripts")]
                public ushort[] Scripts { get; set; }

                public override string ToString()
                {
                    return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
                }

                public enum JobCategory { McAfee, Acronis, vCenter, Custom }
            }
        }
    }
}
