using System;
using HttpApiClient;

namespace VCenterApiClient
{
    public class VCenterClient : ApiClient
    {
        public VCenterClient(Uri baseAddress) : base(baseAddress)
        {
        }
    }
}
