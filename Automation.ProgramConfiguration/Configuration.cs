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
        [System.Xml.Serialization.XmlAttributeAttribute("type")]
        public ConfigurationOutputType OutputType
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
        public bool PasswordFlag
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
        [System.Xml.Serialization.XmlAttributeAttribute("type")]
        public ConfigurationTargetTaskType TaskType
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("script")]
        public string[] Scripts
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
