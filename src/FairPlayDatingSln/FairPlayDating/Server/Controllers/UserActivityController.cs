using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Common.Interfaces;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserActivity;
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
    [ControllerOfEntity(entityName: Constants.EntityNames.UserActivity, primaryKeyType: typeof(long))]
    public partial class UserActivityController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<UserActivityModel[]> GetMyUserActivities(CancellationToken cancellationToken)
        {
            return await this.UserActivityService.GetMyUserActivities()
                .Select(p => this.mapper.Map<UserActivity, UserActivityModel>(p))
                .ToArrayAsync(cancellationToken: cancellationToken);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateMyUserActivity(UserActivityModel userActivityModel, CancellationToken cancellationToken)
        {
            var mappedModel = this.mapper.Map<UserActivityModel, UserActivity>(userActivityModel);
            await this.UserActivityService
                .UpdateMyUserActivityAsync(mappedModel, cancellationToken:cancellationToken);
            return Ok();
        }
    }
}
