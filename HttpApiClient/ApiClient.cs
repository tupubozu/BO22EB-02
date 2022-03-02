using System;
using System.Net.Http;

namespace HttpApiClient
{
    public abstract class ApiClient: IDisposable
    {
        protected internal HttpClient client;

        public ApiClient(Uri baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
