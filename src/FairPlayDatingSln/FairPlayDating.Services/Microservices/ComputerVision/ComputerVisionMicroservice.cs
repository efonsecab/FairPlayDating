using FairPlayDating.Services.Microservices.ComputerVision.Configuration;
using FairPlayDating.Services.Microservices.ComputerVision.Models;
using PTI.Microservices.Library.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FairPlayDating.Services.Microservices.ComputerVision
{
    public class ComputerVisionMicroservice
    {
        private readonly ComputerVisionMicroserviceConfiguration ComputerVisionMicroserviceConfiguration;
        private CustomHttpClient CustomHttpClient;

        public ComputerVisionMicroservice(
            ComputerVisionMicroserviceConfiguration computerVisionMicroserviceConfiguration,
            CustomHttpClient customHttpClient)
        {
            this.ComputerVisionMicroserviceConfiguration = computerVisionMicroserviceConfiguration;
            this.CustomHttpClient = customHttpClient;
        }

        public async Task<AnalyzeImageResponse?> AnalyzeFromImageUrlAsync(Uri imageUrl, CancellationToken cancellationToken)
        {
            var requestUrl = $"{this.ComputerVisionMicroserviceConfiguration.BaseUrl}" +
                $"/ComputerVision/AnalyzeFromImageUrl" +
                $"?imageUrl={HttpUtility.UrlEncode(imageUrl.ToString())}";
            var response = await this.CustomHttpClient.PostAsync(requestUrl, null, 
                cancellationToken: cancellationToken);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<AnalyzeImageResponse>();
            return result;
        }
    }
}
