using ComputerVision.Models;
using Microsoft.AspNetCore.Mvc;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Models.AzureComputerVision.AnalyzeImage;
using PTI.Microservices.Library.Services;

namespace ComputerVision.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputerVisionController : ControllerBase
    {

        private readonly ILogger<ComputerVisionController> _logger;
        private readonly AzureComputerVisionService _azureComputerVisionService;

        public ComputerVisionController(ILogger<ComputerVisionController> logger, 
            AzureComputerVisionService azureComputerVisionService,
            CustomHttpClient customHttpClient)
        {
            _logger = logger;
            _azureComputerVisionService = azureComputerVisionService;
        }


        [HttpPost("[action]")]
        public async Task<AnalyzeImageResponse> AnalyzeFromImageUrl(
            [FromQuery] DetectFaceFromImageUrlRequest model,
            [FromServices] CustomHttpClient customHttpClient,
            CancellationToken cancellationToken)
        {
            var imageBytes = await customHttpClient.GetByteArrayAsync(model.ImageUrl);
            var imageName = Path.GetFileName(model.ImageUrl);
            MemoryStream imageStream = new MemoryStream(imageBytes);
            var result =
            await this._azureComputerVisionService.AnalyzeImageAsync(imageStream,
            visualFeatures: new List<AzureComputerVisionService.VisualFeature>
            {
                AzureComputerVisionService.VisualFeature.Adult,
                AzureComputerVisionService.VisualFeature.Brands,
                AzureComputerVisionService.VisualFeature.Categories,
                AzureComputerVisionService.VisualFeature.Color,
                AzureComputerVisionService.VisualFeature.Description,
                AzureComputerVisionService.VisualFeature.Faces,
                AzureComputerVisionService.VisualFeature.ImageType,
                AzureComputerVisionService.VisualFeature.Objects,
                AzureComputerVisionService.VisualFeature.Tags
            },
            fileName: imageName,
            cancellationToken: cancellationToken);
            return result;
        }
    }
}