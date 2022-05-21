namespace AcronisApiClient
{
    public class AcronisTask
    {
        public string cancelRequested { get; set; }
        public string cancellable { get; set; }
        public string state { get; set; }
        public string completedAt { get; set; }
        public AcronisContext context { get; set; }
        public string enqueuedAt { get; set; }
        public long id { get; set; }
        public string idString { get; set; }
        public Issuer issuer { get; set; }
        public string priority { get; set; }
        public string queue { get; set; }
        public Result result { get; set; }
        public string startedAt { get; set; }
        public string startedByUser { get; set; }
        public Tenant tenant { get; set; }
        public string type { get; set; }
        public string updatedAt { get; set; }
        public string uuid { get; set; }


        public class Issuer
        {
            public string clusterId { get; set; }
            public string id { get; set; }
        }

        public class Result
        {
            public string code { get; set; }
            public string payload { get; set; }
        }

        public class Tenant
        {
            public string id { get; set; }
            public string locator { get; set; }
            public string name { get; set; }
        }
    }
}
