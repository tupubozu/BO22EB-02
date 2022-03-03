using System;
using System.Net.Http.Headers;
using HttpApiClient;

namespace McAfeeEpoClient
{
    public class McAfeeClient: ApiClient
    {
        public McAfeeClient(Uri baseAddress) : this(baseAddress, null)
        {
        }

        public McAfeeClient(Uri baseAddress, string apiKey) : base(baseAddress, apiKey)
        {
            if (!string.IsNullOrEmpty(ApiKey))
                SetAuthHeader(new AuthenticationHeaderValue("", ApiKey));
        }
    }
}
