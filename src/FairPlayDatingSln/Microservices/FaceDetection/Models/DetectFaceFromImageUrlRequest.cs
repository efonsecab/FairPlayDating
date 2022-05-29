using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.ComponentModel.DataAnnotations;

namespace FaceDetection.Models
{
    public class DetectFaceFromImageUrlRequest
    {
        [Url]
        [Required]
        public string? ImageUrl { get; set; }
    }
}
