using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using PTI.Microservices.Library.Services;

namespace FaceDetection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaceDetectionController : ControllerBase
    {

        private readonly ILogger<FaceDetectionController> _logger;
        private readonly AzureFaceService _azureFaceService;

        public FaceDetectionController(ILogger<FaceDetectionController> logger, 
            AzureFaceService azureFaceService)
        {
            _logger = logger;
            _azureFaceService = azureFaceService;
        }

        [HttpPost("[action]")]
        public async Task<IList<DetectedFace>> DetectFaceFromImageUrl([FromQuery]DetectFaceFromImageUrlRequest model,
            CancellationToken cancellationToken)
        {
            var imageUrl = new Uri(model.ImageUrl!);
            var result = 
            await this._azureFaceService
                .DetectFacesAsync(imageUri: imageUrl, cancellationToken: cancellationToken);
            return result;
        }
    }
}