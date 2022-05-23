using AutoMapper;
using FairPlayDating.DataAccess.Data;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.Activity;
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
        private FairPlayDatingDbContext _fairPlayDatingDbContext;
        private IMapper _mapper;

        public ActivityController(FairPlayDatingDbContext fairPlayDatingDbContext, 
            IMapper mapper)
        {
            _fairPlayDatingDbContext = fairPlayDatingDbContext;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<ActivityModel[]> GetAllActivities(CancellationToken cancellationToken)
        {
            var result = await _fairPlayDatingDbContext.Activity
                .Select(p => _mapper.Map<Activity, ActivityModel>(p))
                .ToArrayAsync(cancellationToken:cancellationToken);
            return result;
        }
    }
}
