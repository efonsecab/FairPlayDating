using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTI.Microservices.Library.Models.FacebookGraph.GetMyAlbums;
using PTI.Microservices.Library.Models.FacebookGraph.GetMyPhotos;
using PTI.Microservices.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FairPlayDating.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacebookController : ControllerBase
    {
        private FacebookGraphService FacebookGraphService { get; }
        public FacebookController(FacebookGraphService facebookGraphService)
        {
            this.FacebookGraphService = facebookGraphService;
        }

        [HttpGet("[action]")]
        public async Task<GetMyPhotosResponse> GetMyPhotos(string pageToken=null, CancellationToken cancellationToken=default)
        {
            var result = await this.FacebookGraphService.GetMyPhotosAsync(pageToken, cancellationToken);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<GetMyAlbumsResponse> GetMyAlbums(string pageToken = null, CancellationToken cancellationToken=default)
        {
            var result = await this.FacebookGraphService.GetMyAlbumsAsync(pageToken, cancellationToken);
            var json = JsonSerializer.Serialize(result);
            return result;
        }
    }
}
