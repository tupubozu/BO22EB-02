using System;
using System.Net.Http.Headers;
using HttpApiClient;

namespace AcronisApiClient
{
    public class AcronisClient : ApiClient
    {
        public AcronisClient(Uri baseAddress) : this(baseAddress, null)
        {
        }

        public AcronisClient(Uri baseAddress, string apiKey) : base(baseAddress, apiKey)
        {
            if (!string.IsNullOrEmpty(ApiKey))
                SetAuthHeader(new AuthenticationHeaderValue("", ApiKey));
        }
    }
}
