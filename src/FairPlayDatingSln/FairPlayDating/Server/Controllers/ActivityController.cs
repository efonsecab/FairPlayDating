using AutoMapper;
using FairPlayDating.DataAccess.Data;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Activity;
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
    public class ActivityController : ControllerBase
    {
        private IMapper _mapper;
        private ActivityService _activityService;

        public ActivityController(ActivityService activityService, 
            IMapper mapper)
        {
            _mapper = mapper;
            _activityService = activityService;
        }

        [HttpGet("[action]")]
        public async Task<ActivityModel[]> GetAllActivities(CancellationToken cancellationToken)
        {
            var result = await this._activityService
                .GetAllActivity(trackEntities:false, cancellationToken: cancellationToken)
                .Select(p=> _mapper.Map<Activity,ActivityModel>(p))
                .ToArrayAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
