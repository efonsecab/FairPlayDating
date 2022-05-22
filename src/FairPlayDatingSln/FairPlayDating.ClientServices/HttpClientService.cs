using FairPlayDating.Common.Global;
using System.Net.Http;

namespace FairPlayDating.ClientServices
{
    public class HttpClientService
    {
        private const string AssemblyName = Constants.Names.ApplicationName;
        private IHttpClientFactory HttpClientFactory { get; }
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            this.HttpClientFactory = httpClientFactory;
        }

        public HttpClient CreateAnonymousClient()
        {
            return this.HttpClientFactory.CreateClient($"{AssemblyName}.ServerAPI.Anonymous");
        }

        public HttpClient CreateAuthorizedClient()
        {
            return this.HttpClientFactory.CreateClient($"{AssemblyName}.ServerAPI");
        }
    }
}