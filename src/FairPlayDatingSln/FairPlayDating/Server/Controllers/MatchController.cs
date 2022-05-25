using AutoMapper;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Match;
using FairPlayDating.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FairPlayDating.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchController : ControllerBase
    {
        private readonly MatchService _matchService;
        private readonly IMapper _mapper;
        public MatchController(MatchService matchService, IMapper mapper)
        {
            this._matchService = matchService;
            this._mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<MatchModel[]> GetMyMatches(CancellationToken cancellationToken)
        {
            var matches = await this._matchService.GetMyMatchesAsync(cancellationToken: cancellationToken);
            var result = await matches
                .Select(p => this._mapper.Map<UserProfile,MatchModel>(p))
                .ToArrayAsync();
            return result;
        }
    }
}
