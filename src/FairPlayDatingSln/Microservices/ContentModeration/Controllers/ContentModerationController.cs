using ContentModeration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using PTI.Microservices.Library.Models.AzureContentModeratorService;
using PTI.Microservices.Library.Services;

namespace ContentModeration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentModerationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ContentModerationController> _logger;
        private readonly AzureContentModeratorService _azureContentModeratorService;

        public ContentModerationController(ILogger<ContentModerationController> logger,
            AzureContentModeratorService azureContentModeratorService)
        {
            _logger = logger;
            _azureContentModeratorService = azureContentModeratorService;
        }

        [HttpPost("[action]")]
        public async Task<AnalyzeTextExpandedResponse> AnalyzePlainTextAsync(
            [FromRoute]AnalyzeTextRequest model, 
            CancellationToken cancellationToken)
        {
            var result = await this._azureContentModeratorService.AnalyzeTextAsync(
                text:model.Text, textType: AzureContentModeratorService.TextType.PlainText,
                textLanguage: model.TextLanguage,
                cancellationToken: cancellationToken);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<AnalyzeTextExpandedResponse> AnalyzeHtmlAsync(
            [FromBody] AnalyzeTextRequest model, CancellationToken cancellationToken)
        {
            var result = await this._azureContentModeratorService.AnalyzeTextAsync(
                text: model.Text,
                textType: AzureContentModeratorService.TextType.Html,
                textLanguage: model.TextLanguage,
                cancellationToken: cancellationToken);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<AnalyzeTextExpandedResponse> AnalyzeXmlAsync(
            [FromBody] AnalyzeTextRequest model, CancellationToken cancellationToken)
        {
            var result = await this._azureContentModeratorService.AnalyzeTextAsync(
                text:model.Text,
                textType: AzureContentModeratorService.TextType.Xml,
                textLanguage: model.TextLanguage,
                cancellationToken: cancellationToken);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<AnalyzeTextExpandedResponse> AnalyzeMarkdownAsync(
            [FromBody] AnalyzeTextRequest model, CancellationToken cancellationToken)
        {
            var result = await this._azureContentModeratorService.AnalyzeTextAsync(
                text: model.Text,
                textType: AzureContentModeratorService.TextType.Markdown,
                textLanguage: model.TextLanguage,
                cancellationToken: cancellationToken);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<Evaluate> AnalyzeImageFromUrlAsync(
            AnalyzeImageFromUrlRequest model, [FromServices] HttpClient httpClient)
        {
            Stream imageStream = await httpClient.GetStreamAsync(model.ImageUrl);
            var result = await this._azureContentModeratorService.AnalyzeImageAsync(imageStream);
            return result;
        }
    }
}