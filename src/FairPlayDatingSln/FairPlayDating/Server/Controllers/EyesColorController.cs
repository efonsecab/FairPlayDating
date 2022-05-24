﻿using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FairPlayDating.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ControllerOfEntity(entityName: Constants.EntityNames.EyesColor, primaryKeyType: typeof(short))]
    public partial class EyesColorController : ControllerBase
    {
    }
}
