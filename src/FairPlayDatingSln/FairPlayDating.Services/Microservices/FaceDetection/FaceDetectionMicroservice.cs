using FairPlayDating.Services.Microservices.FaceDetection.Configuration;
using PTI.Microservices.Library.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services.Microservices.FaceDetection
{
    public class FaceDetectionMicroservice
    {
        private FaceDetectionMicroserviceConfiguration FaceDetectionMicroserviceConfiguration { get; }
        private CustomHttpClient CustomHttpClient { get; }

        public FaceDetectionMicroservice(
            FaceDetectionMicroserviceConfiguration faceDetectionMicroserviceConfiguration,
            CustomHttpClient customHttpClient)
        {
            this.FaceDetectionMicroserviceConfiguration = faceDetectionMicroserviceConfiguration;
            this.CustomHttpClient = customHttpClient;
        }

        public async Task<string> DetectFacesFromImageUrlAsync(string imageUrl, CancellationToken cancellationToken)
        {
            var imageBytes = await new HttpClient().GetByteArrayAsync(imageUrl);
            MemoryStream imageStream = new MemoryStream(imageBytes);
            var requestUrl = $"{this.FaceDetectionMicroserviceConfiguration.BaseUrl}/DetectFaces";
            string contentType = "image/jpeg";
            var response = await this.CustomHttpClient.PostImageAsync(requestUrl, imageStream, contentType, cancellationToken);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }
    }
}
