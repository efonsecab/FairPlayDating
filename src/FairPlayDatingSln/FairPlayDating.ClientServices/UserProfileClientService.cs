using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.ClientServices
{
    [ClientServiceOfEntity(entityName: Constants.EntityNames.UserProfile, primaryKeyType: typeof(long))]
    public partial class UserProfileClientService
    {
        public async Task<UserProfileModel?> GetMyUserProfileAsync()
        {
            var authorizedHttpClient = _httpClientService.CreateAuthorizedClient();
            var requestUrl = "api/UserProfile/GetMyUserProfile";
            var result = await authorizedHttpClient
                .GetFromJsonAsync<UserProfileModel?>(requestUrl);
            return result;
        }
    }
}
