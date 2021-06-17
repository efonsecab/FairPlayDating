﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FairPlayDating.Client.ClientServices
{
    public class HttpClientService
    {
        private const string AssemblyName = "FairPlayDating";
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
