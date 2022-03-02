using System;
using HttpApiClient;

namespace McAfeeEpoClient
{
    public class McAfeeClient: ApiClient
    {
        public McAfeeClient(Uri baseAddress): base(baseAddress)
        {
            
        }
    }
}
