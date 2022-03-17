namespace Automation.Configuration
{
    public partial class ProgramConfiguration
    {
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


}
