using System.ComponentModel.DataAnnotations;

namespace ContentModeration.Models
{
    public class AnalyzeImageFromUrlRequest
    {
        [Required]
        [Url]
        public string? ImageUrl { get; set; }
    }
}
