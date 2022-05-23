using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.ClientServices
{
    [ClientServiceOfEntity(entityName: Constants.EntityNames.Activity, primaryKeyType: typeof(long))]
    public partial class ActivityClientService
    {
    }
}
