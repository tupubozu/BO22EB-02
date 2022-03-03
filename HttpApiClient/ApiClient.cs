using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HttpApiClient
{
    public abstract class ApiClient: IDisposable
    {
        private HttpClient client;
        private JsonSerializer serializer;
        protected internal string ApiKey;

        public ApiClient(Uri baseAddress) : this (baseAddress: baseAddress, apiKey: null)
        {
        }

        public ApiClient(Uri baseAddress, string apiKey)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            ApiKey = apiKey;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        public void Dispose()
        {
            client.Dispose();
        }

        protected internal void SetAuthHeader(AuthenticationHeaderValue authHeader)
        {
            client.DefaultRequestHeaders.Authorization = authHeader;
        }

        protected internal async Task<T> GetObjectAsync<T>(string uri)
        {
            T apiResponseData;

            using (var a = await client.GetStreamAsync(uri))
            using (var b = new StreamReader(a))
            using (var c = new JsonTextReader(b))
            {
                apiResponseData = serializer.Deserialize<T>(c);
            }

            return apiResponseData;
        }

        protected internal async Task<Tr> PostObjectAsync<Tr, Ts>(string uri, Ts apiRequestData)
        {
            Tr apiResponseData;

            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
                serializer.Serialize(writer, apiRequestData, typeof(Ts));

            using (var a = await client.PostAsync(uri, new StringContent(builder.ToString())))
            using (var b = new StreamReader(await a.Content.ReadAsStreamAsync()))
            using (var c = new JsonTextReader(b))
            {
                apiResponseData = serializer.Deserialize<Tr>(c);
            }

            return apiResponseData;
        }

        protected internal async Task<Tr> PutObjectAsync<Tr, Ts>(string uri, Ts apiRequestData)
        {
            Tr apiResponseData;

            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
                serializer.Serialize(writer, apiRequestData, typeof(Ts));

            using (var a = await client.PutAsync(uri, new StringContent(builder.ToString())))
            using (var b = new StreamReader(await a.Content.ReadAsStreamAsync()))
            using (var c = new JsonTextReader(b))
            {
                apiResponseData = serializer.Deserialize<Tr>(c);
            }

            return apiResponseData;
        }

        protected internal async Task<T> DeleteObjectAsync<T>(string uri)
        {
            T apiResponseData;

            using (var a = await client.DeleteAsync(uri))
            using (var b = new StreamReader(await a.Content.ReadAsStreamAsync()))
            using (var c = new JsonTextReader(b))
            {
                apiResponseData = serializer.Deserialize<T>(c);
            }

            return apiResponseData;
        }
    }
}
