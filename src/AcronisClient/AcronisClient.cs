using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HttpApiClient;
using Newtonsoft.Json;

namespace AcronisApiClient
{
    public class AcronisClient : ApiClient
    {
        string SessionKey;

        readonly string ApiRelativeUri = "/api/task_manager/v2";

        public AcronisClient(Uri baseAddress) : this(baseAddress, null)
        {
        }

        public AcronisClient(Uri baseAddress, string apiKey) : base(baseAddress)
        {
            SessionKey = apiKey;
            //if (!string.IsNullOrEmpty(ApiKey))
            //    SetAuthHeader(new AuthenticationHeaderValue("", ApiKey));
        }

        private class SessionParameters
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_on")]
            public int Expires { get; set; }

            [JsonProperty("id_token")]
            public string IdToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

        public void CreateSession() => CreateSessionAsync().GetAwaiter().GetResult();

        public async Task CreateSessionAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, "/idp/token"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", SessionKey);
                request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                request.Content = new ByteArrayContent(Encoding.ASCII.GetBytes("grant_type=client_credentials"));
                var session = await RequestObjectAsync<SessionParameters>(request);
                ApiKey = session.AccessToken;
            }
        }

        private class ApiArrayResponseObject<T>
        {
            [JsonProperty("items")]
            public T[] Items { get; set; }
        }

        public async Task<AcronisTask[]> ListAllTasks()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, ApiRelativeUri + "/tasks"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = await RequestObjectAsync<ApiArrayResponseObject<AcronisTask>>(request);
                return data.Items;
            }
        }

        public async Task<AcronisActivity[]> ListAllActivities()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, ApiRelativeUri + "/activities"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = await RequestObjectAsync<ApiArrayResponseObject<AcronisActivity>>(request);
                return data.Items;
            }
        }
    }
}
