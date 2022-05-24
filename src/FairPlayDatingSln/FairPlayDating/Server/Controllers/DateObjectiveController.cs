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
    [ControllerOfEntity(entityName: Constants.EntityNames.DateObjective, primaryKeyType: typeof(short))]
    public partial class DateObjectiveController : ControllerBase
    {
    }
}
