using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.UserActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.ClientServices
{
    [ClientServiceOfEntity(Constants.EntityNames.UserActivity, primaryKeyType: typeof(long))]
    public partial class UserActivityClientService
    {
        public async Task<UserActivityModel[]?> GetMyUserActivitiesAsync()
        {
            var authorizedHttpClient = _httpClientService.CreateAuthorizedClient();
            var requestUrl = "api/UserActivity/GetMyUserActivities";
            var result = await authorizedHttpClient
                .GetFromJsonAsync<UserActivityModel[]>(requestUrl);
            return result;
        }
        public async Task UpdateMyUserActivityAsync(UserActivityModel userActivityModel)
        {
            var authorizedHttpClient = _httpClientService.CreateAuthorizedClient();
            var requestUrl = "api/UserActivity/UpdateMyUserActivity";
            var response = await authorizedHttpClient.PutAsJsonAsync(requestUrl, userActivityModel);
            response.EnsureSuccessStatusCode();
        }
    }
}
