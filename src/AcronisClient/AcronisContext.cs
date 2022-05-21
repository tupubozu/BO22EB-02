namespace AcronisApiClient
{
    public class AcronisContext
    {
        public string IsProcessRoot { get; set; }
        public Agent Persistent { get; set; }
        public string Specific { get; set; }
        public string UserName { get; set; }
        public string isLegacy { get; set; }
        public string title { get; set; }


        public class Agent
        {
            public string AgentType { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string OwnerID { get; set; }
        }
    }


}
