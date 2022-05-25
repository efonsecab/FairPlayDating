using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
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
    [ControllerOfEntity(entityName: Constants.EntityNames.UserProfile, primaryKeyType: typeof(long))]
    public partial class UserProfileController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<UserProfileModel> GetMyUserProfile(CancellationToken cancellationToken)
        {
            var query = this.UserProfileService.GetMyUserProfile();
            var result = await query.AsNoTracking()
                .Select(p => this.mapper.Map<UserProfile, UserProfileModel>(p))
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return result;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateMyUserProfile(UserProfileModel userProfileModel, CancellationToken cancellationToken)
        {
            var mappedUserProfile = this.mapper.Map<UserProfileModel, UserProfile>(userProfileModel);
            await this.UserProfileService.UpdateMyUserProfileAsync(mappedUserProfile, cancellationToken);
            return Ok();
        }
    }
}
