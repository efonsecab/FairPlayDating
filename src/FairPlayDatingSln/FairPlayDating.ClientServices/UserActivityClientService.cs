using FairPlayDating.Common.CustomAttributes;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.UserActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static FairPlayDating.Common.Global.Constants;
using static FairPlayDating.Common.Global.Constants.ApiRoutes.UserActivityController;
namespace FairPlayDating.ClientServices
{
    [ClientServiceOfEntity(Constants.EntityNames.UserActivity, primaryKeyType: typeof(long))]
    public partial class UserActivityClientService
    {
        public async Task<UserActivityModel[]?> GetMyUserActivitiesAsync()
        {
            var authorizedHttpClient = _httpClientService.CreateAuthorizedClient();
            var result = await authorizedHttpClient
                .GetFromJsonAsync<UserActivityModel[]>($"{GetMyUserActivities}");
            return result;
        }
        public async Task UpdateMyUserActivityAsync(UserActivityModel userActivityModel)
        {
            var authorizedHttpClient = _httpClientService.CreateAuthorizedClient();
            var response = await authorizedHttpClient.PutAsJsonAsync($"{UpdateMyUserActivity}", userActivityModel);
            response.EnsureSuccessStatusCode();
        }
    }
}
