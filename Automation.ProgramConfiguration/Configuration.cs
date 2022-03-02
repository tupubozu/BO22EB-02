namespace Automation.ProgramConfiguration
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Configuration
    {

        private ConfigurationOutput[] optionsField;

        private ConfigurationTarget[] workField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Output", IsNullable = false)]
        public ConfigurationOutput[] Options
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
        public ConfigurationTarget[] Work
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
    }

    public enum ConfigurationOutputType { File, Email, Console }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigurationOutput
    {

        private ConfigurationOutputType typeField;

        private string addressField;

        private bool passwordField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("type", typeof(ConfigurationOutputType))]
        public ConfigurationOutputType type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("address", typeof(string))]
        public string address
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
        [System.Xml.Serialization.XmlAttributeAttribute("password", typeof(bool))]
        public bool password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigurationTarget
    {

        private ConfigurationTargetTask[] taskField;

        private string hostField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Task")]
        public ConfigurationTargetTask[] Task
        {
            get
            {
                return this.taskField;
            }
            set
            {
                this.taskField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("host", typeof(string))]
        public string host
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

    public enum ConfigurationTargetTaskType { McAfee, Acronis, vCenter, Custom }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ConfigurationTargetTask
    {

        private ConfigurationTargetTaskType typeField;

        private ushort portField;

        private string keyField;

        private string[] scriptField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("type", typeof(ConfigurationTargetTaskType))]
        public ConfigurationTargetTaskType type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("port", typeof(ushort))]
        public ushort port
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
        [System.Xml.Serialization.XmlAttributeAttribute("key", typeof(string))]
        public string key
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
        [System.Xml.Serialization.XmlAttributeAttribute("script", typeof(string[]))]
        public string[] script
        {
            get
            {
                return this.scriptField;
            }
            set
            {
                this.scriptField = value;
            }
        }
    }
}
