using FairPlayDating.Services.Microservices.ComputerVision.Configuration;
using PTI.Microservices.Library.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<string> AnalyzeFromImageUrlAsync(Uri imageUrl, CancellationToken cancellationToken)
        {
            var imageBytes = await new HttpClient().GetByteArrayAsync(imageUrl);
            MemoryStream imageStream = new MemoryStream(imageBytes);
            var requestUrl = $"{this.ComputerVisionMicroserviceConfiguration.BaseUrl}" +
                $"/AnalyzeFromImageUrl/AnalyzeFromImageUrl";
            string contentType = "image/jpeg";
            var response = await this.CustomHttpClient.PostImageAsync(requestUrl, imageStream, contentType, cancellationToken);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }
    }
}
