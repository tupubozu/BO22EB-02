using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;

namespace HttpApiClient
{
    public abstract class ApiClient: IDisposable
    {
        private HttpClient client;
        private JsonSerializer serializer;
        protected internal string ApiKey;

        protected internal Uri BaseAddress { get => client.BaseAddress; }

        public ApiClient(Uri baseAddress) : this (baseAddress: baseAddress, apiKey: null)
        {
        }

        public ApiClient(Uri baseAddress, string apiKey)
        {
            serializer = new JsonSerializer();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            ApiKey = apiKey;
            
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json"); // Migth be required to be moved into a HttpRequest object in each of the sub-classes' API calls (methods)
        }

        public virtual void Dispose()
        {
            client.Dispose();
        }

        protected internal void SetAuthHeader(AuthenticationHeaderValue authHeader)
        {
            client.DefaultRequestHeaders.Authorization = authHeader;
        }

        protected internal void SetHttpHeader(string name, IEnumerable<string> values)
        {
            client.DefaultRequestHeaders.Add(name, values);
        }

        protected internal void SetHttpHeader(string name, string value)
        {
            client.DefaultRequestHeaders.Add(name, value);
        }

        protected internal async Task RequestAsync(HttpRequestMessage apiRequest)
        {
            using (var response = (await client.SendAsync(apiRequest)).EnsureSuccessStatusCode())
                return;
        }

        protected internal async Task<T> RequestObjectAsync<T>(HttpRequestMessage apiRequest)
        {
            using (var response = (await client.SendAsync(apiRequest)).EnsureSuccessStatusCode())
            using (var a = await response.Content.ReadAsStreamAsync())
            using (var b = new StreamReader(a))
                return JsonConvert.DeserializeObject<T>(await b.ReadToEndAsync());
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
