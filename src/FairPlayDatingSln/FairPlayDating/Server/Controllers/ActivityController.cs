using AutoMapper;
using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
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
    [ControllerOfEntity(entityName:Constants.EntityNames.Activity, primaryKeyType:typeof(long))]
    public partial class ActivityController : ControllerBase
    {
    }
}
