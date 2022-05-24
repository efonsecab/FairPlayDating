using FairPlayDating.ClientServices;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Pages
{
    [Route(Constants.MauiBlazorAppRoutes.MyUserProfile)]
    [Authorize]
    public partial class MyUserProfile
    {
        public UserProfileModel MyUserProfileModel { get; private set; }
        [Inject]
        private UserProfileClientService UserProfileClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.MyUserProfileModel = await this.UserProfileClientService.GetMyUserProfileAsync();
            }
            catch (Exception)
            {
                this.MyUserProfileModel = new();
            }
        }

        private async Task UpdateMyUserProfileAsync()
        {
            //await this.UserProfileClientService.UpdateMyUserProfileAsync(this.MyUserProfileModel);
        }
    }
}
