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
            private static ushort NextID = 0;

            public Script()
            {
                ID = NextID++;
            }

            [XmlAttribute("id")]
            public ushort ID { get; set; }

            [XmlAttribute("name")]
            public string Name { get; set; }

            [XmlAttribute("src")]
            public string Source { get; set; }

            [XmlAttribute("crypt_key")]
            public byte[] EncryptionKey { get; set; }

            [XmlAttribute("crypt_iv")]
            public byte[] EncryptionIV { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
            }
        }
    }
}
