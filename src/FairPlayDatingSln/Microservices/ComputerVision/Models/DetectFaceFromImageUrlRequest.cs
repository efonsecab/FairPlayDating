using System.ComponentModel.DataAnnotations;

namespace ComputerVision.Models
{
    public class DetectFaceFromImageUrlRequest
    {
        [Url]
        [Required]
        public string? ImageUrl { get; set; }
    }
}
