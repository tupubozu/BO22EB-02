using System;
using HttpApiClient;

namespace AcronisApiClient
{
    public class AcronisClient : ApiClient
    {
        public AcronisClient(Uri baseAddress) : base(baseAddress)
        {
        }
    }
}
