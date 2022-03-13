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

        private ProgramConfigurationOutput[] optionsField;

        private ProgramConfigurationTarget[] workField;

        private ProgramConfigurationMetadata metadataField;

        private ProgramConfigurationScript[] scriptsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Output", IsNullable = false)]
        public ProgramConfigurationOutput[] Options
        {
            get
            {
                return this.optionsField;
            }
            set
            {
                this.optionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Target", IsNullable = false)]
        public ProgramConfigurationTarget[] Work
        {
            get
            {
                return this.workField;
            }
            set
            {
                this.workField = value;
            }
        }

        /// <remarks/>
        public ProgramConfigurationMetadata Metadata
        {
            get
            {
                return this.metadataField;
            }
            set
            {
                this.metadataField = value;
            }
        }


        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Script", IsNullable = false)]
        public ProgramConfigurationScript[] Scripts
        {
            get
            {
                return this.scriptsField;
            }
            set
            {
                this.scriptsField = value;
            }
        }

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

    }

    public enum ConfigurationOutputCategory { File, Email, Console }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProgramConfigurationOutput
    {

        private ConfigurationOutputCategory categoryField;

        private string addressField;

        private bool passwordField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("category")]
        public ConfigurationOutputCategory Category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("address")]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("password")]
        public bool? Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value ?? false;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProgramConfigurationTarget
    {

        private ProgramConfigurationTargetJob[] jobField;

        private string hostField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Job")]
        public ProgramConfigurationTargetJob[] Job
        {
            get
            {
                return this.jobField;
            }
            set
            {
                this.jobField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("host")]
        public string Host
        {
            get
            {
                return this.hostField;
            }
            set
            {
                this.hostField = value;
            }
        }
    }

    public enum ProgramConfigurationTargetJobCategory { McAfee, Acronis, vCenter, Custom }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProgramConfigurationTargetJob
    {

        private ProgramConfigurationTargetJobCategory categoryField;

        private ushort portField;

        private string keyField;

        private string usernameField;

        private string[] scriptsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("category")]
        public ProgramConfigurationTargetJobCategory Category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("port")]
        public ushort Port
        {
            get
            {
                return this.portField;
            }
            set
            {
                this.portField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("key")]
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute("username")]
        public string Username
        {
            get
            {
                return this.usernameField;
            }
            set
            {
                this.usernameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("scripts")]
        public string[] Scripts
        {
            get
            {
                return this.scriptsField;
            }
            set
            {
                this.scriptsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProgramConfigurationMetadata
    {

        private string descriptionField;

        private System.DateTime revisionField;

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Revision
        {
            get
            {
                return this.revisionField;
            }
            set
            {
                this.revisionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProgramConfigurationScript
    {

        private ushort idField;

        private string srcField;

        private byte[] keyField;

        private byte[] ivField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public ushort Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("src")]
        public string Source
        {
            get
            {
                return this.srcField;
            }
            set
            {
                this.srcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("key")]
        public byte[] Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("iv")]
        public byte[] iv
        {
            get
            {
                return this.ivField;
            }
            set
            {
                this.ivField = value;
            }
        }
    }


}
