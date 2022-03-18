namespace Automation.Configuration
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "ProgramConfiguration")]
    public partial class ProgramConfiguration
    {
        public ConfigurationMetadata Metadata { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("Output", IsNullable = false)]
        public Output[] Options { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("Target", IsNullable = false)]
        public Target[] Work { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("Key", IsNullable = false)]
        public Key[] Keys { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("Script", IsNullable = false)]
        public Script[] Scripts { get; set; }

        public static bool TryParse(System.IO.Stream stream, out ProgramConfiguration config)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ProgramConfiguration));
                config = serializer.Deserialize(stream) as ProgramConfiguration;
                return true;
            }
            catch (System.Exception)
            {
                config = null;
                return false;
            }
        }

        public static ProgramConfiguration Parse(System.IO.Stream stream)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ProgramConfiguration));
            return serializer.Deserialize(stream) as ProgramConfiguration;
        }

        public override string ToString()
        {
            return Metadata is null ? base.ToString() : $"Configuration {Metadata.Title}";
        }


        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class ConfigurationMetadata
        {
            public string Title { get; set; }
        
            public string Description { get; set; }
        
            public System.DateTime Revision { get; set; }

            public MetadataAuthor Author { get; set; }

            [System.SerializableAttribute()]
            [System.ComponentModel.DesignerCategoryAttribute("code")]
            [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
            public partial class MetadataAuthor
            {
                public string Name { get; set; }
                
                public string Email { get; set; }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class Output
        {
            [System.Xml.Serialization.XmlAttributeAttribute("category")]
            public OutputCategory Category { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("address")]
            public string Address { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("password")]
            public bool Password { get; set; }

            public enum OutputCategory { File, Email, Console }

        }

        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class Target
        {
            [System.Xml.Serialization.XmlElementAttribute("Job")]
            public Job[] Jobs { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("host")]
            public string Host { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Host) ? base.ToString() : Host;
            }


            /// <remarks/>
            [System.SerializableAttribute()]
            [System.ComponentModel.DesignerCategoryAttribute("code")]
            [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
            public class Job
            {
                [System.Xml.Serialization.XmlAttributeAttribute("name")]
                public string Name { get; set; }

                [System.Xml.Serialization.XmlAttributeAttribute("category")]
                public JobCategory Category { get; set; }

                [System.Xml.Serialization.XmlAttributeAttribute("port")]
                public ushort Port { get; set; }

                [System.Xml.Serialization.XmlAttributeAttribute("keys")]
                public byte[] Keys { get; set; }

                [System.Xml.Serialization.XmlAttributeAttribute("scripts")]
                public ushort[] Scripts { get; set; }

                public override string ToString()
                {
                    return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
                }

                public enum JobCategory { McAfee, Acronis, vCenter, Custom }
            }

        }

        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class Key
        {
            [System.Xml.Serialization.XmlAttributeAttribute("id")]
            public byte ID { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("name")]
            public string Name { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("category")]
            public string Category { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("src")]
            public string Source { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("user")]
            public string User { get; set; }

            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
            }
        }

        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public class Script
        {
            [System.Xml.Serialization.XmlAttributeAttribute("id")]
            public ushort ID { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("name")]
            public string Name { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("src")]
            public string Source { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("key")]
            public string Key { get; set; }

            [System.Xml.Serialization.XmlAttributeAttribute("iv")]
            public string IV { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Name) ? base.ToString() : Name;
            }
        }
    }

}
