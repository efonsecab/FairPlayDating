using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FairPlayDating.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ControllerOfEntity(entityName: Constants.EntityNames.UserPhoto, primaryKeyType: typeof(long))]
    public partial class UserPhotoController : ControllerBase
    {
    }
}
