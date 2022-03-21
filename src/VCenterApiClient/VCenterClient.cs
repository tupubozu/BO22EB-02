using System;
using System.Net.Http.Headers;
using HttpApiClient;
using System.Threading.Tasks;

namespace VCenterApiClient
{
    public class VCenterClient : ApiClient
    {
        public VCenterClient(Uri baseAddress) : this(baseAddress, null)
        {
        }

        public VCenterClient(Uri baseAddress, string apiKey) : base(baseAddress, apiKey)
        {
            if (!string.IsNullOrEmpty(ApiKey))
                SetAuthHeader(new AuthenticationHeaderValue("vmware-api-session-id", ApiKey));
        }

        public VMSummmary[] ListVm() => ListVmAsync().GetAwaiter().GetResult();
        public async Task<VMSummmary[]> ListVmAsync() => await GetObjectAsync<VMSummmary[]>("/api/vcenter/vm");
        
    }
}
