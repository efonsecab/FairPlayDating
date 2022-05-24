using FairPlayDating.ClientServices;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.EyesColor;
using FairPlayDating.Models.HairColor;
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
        public HairColorModel[] AllHairColors { get; private set; }
        public EyesColorModel[] AllEyesColors { get; private set; }
        [Inject]
        private UserProfileClientService UserProfileClientService { get; set; }
        [Inject]
        private HairColorClientService HairColorClientService { get; set; }
        [Inject]
        private EyesColorClientService EyesColorClientService { get; set; }

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
            this.AllHairColors = await this.HairColorClientService.GetAllHairColorAsync();
            this.AllEyesColors = await this.EyesColorClientService.GetAllEyesColorAsync();
        }

        private async Task UpdateMyUserProfileAsync()
        {
            //await this.UserProfileClientService.UpdateMyUserProfileAsync(this.MyUserProfileModel);
        }
    }
}
