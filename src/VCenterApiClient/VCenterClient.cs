using System;
using System.Net.Http;
using System.Net.Http.Headers;
using HttpApiClient;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VCenterApiClient
{
    public class VCenterClient : ApiClient
    {

        string SessionKey;

        public VCenterClient(Uri baseAddress) : this(baseAddress, null)
        {
        }

        public VCenterClient(Uri baseAddress, string apiKey) : base(baseAddress)
        {
            SessionKey = apiKey;
            //if (!string.IsNullOrEmpty(ApiKey))
                //SetHttpHeader("vmware-api-session-id", ApiKey);
        }

        
        private protected sealed class SessionParameters
        {
            [JsonProperty("password")]
            public string Password { get; set; }
        }

        public void CreateSession() => CreateSessionAsync().GetAwaiter().GetResult();

        public async Task CreateSessionAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, "/api/session"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", SessionKey);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var session = await RequestObjectAsync<SessionParameters>(request);
                ApiKey = session.Password;
            }
        }

        public void DeleteSession() => DeleteSessionAsync().GetAwaiter().GetResult();

        public async Task DeleteSessionAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, "/api/session"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", SessionKey);
                await RequestAsync(request);
            }
        }

        public VMSummmary[] ListVm() => ListVmAsync().GetAwaiter().GetResult();
        public async Task<VMSummmary[]> ListVmAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/api/vcenter/vm"))
            {
                request.Headers.Add("Accept", "application/json");
                return await RequestObjectAsync<VMSummmary[]>(request);
            }
        }

        public override void Dispose()
        {
            try
            {
                DeleteSession();
            }
            catch (Exception) { }
            finally
            {
                base.Dispose();
            }
        }
    }
}
