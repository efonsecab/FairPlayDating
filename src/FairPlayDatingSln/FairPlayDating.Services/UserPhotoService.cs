using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services
{
    [ServiceOfEntity(entityName: Constants.EntityNames.UserPhoto, primaryKeyType: typeof(long))]
    public partial class UserPhotoService
    {
    }
}
