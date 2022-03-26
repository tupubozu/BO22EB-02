using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ReGen.Configuration
{
    public partial class ProgramConfiguration
    {
        [Serializable()]
        [DesignerCategory("code")]
        [XmlType(AnonymousType = true)]
        public class ConfigurationMetadata
        {
            public string Title { get; set; }
        
            public string Description { get; set; }
        
            public System.DateTime Revision { get; set; }

            public MetadataAuthor Author { get; set; }

            [Serializable()]
            [DesignerCategory("code")]
            [XmlType(AnonymousType = true)]
            public class MetadataAuthor
            {
                public string Name { get; set; }
                
                public string Email { get; set; }
            }
        }
    }
}
