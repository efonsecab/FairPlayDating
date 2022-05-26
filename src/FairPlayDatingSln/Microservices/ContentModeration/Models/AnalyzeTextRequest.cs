using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static PTI.Microservices.Library.Services.AzureContentModeratorService;

namespace ContentModeration.Models
{
    public class AnalyzeTextRequest
    {
        [Required]
        [FromBody]
        public string? Text { get; set; }
        [Required]
        [FromQuery]
        public TextLanguage TextLanguage { get; set; }
    }
}
