using FairPlayDating.ClientServices;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.DateObjective;
using FairPlayDating.Models.EyesColor;
using FairPlayDating.Models.Gender;
using FairPlayDating.Models.HairColor;
using FairPlayDating.Models.KidStatus;
using FairPlayDating.Models.Religion;
using FairPlayDating.Models.TattooStatus;
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
        public GenderModel[] AllGenders { get; private set; }
        public DateObjectiveModel[] AllDateObjectives { get; private set; }
        public ReligionModel[] AllReligions { get; private set; }
        public KidStatusModel[] AllKidStatus { get; private set; }
        public TattooStatusModel[] AllTatooStatus { get; private set; }
        [Inject]
        private UserProfileClientService UserProfileClientService { get; set; }
        [Inject]
        private HairColorClientService HairColorClientService { get; set; }
        [Inject]
        private EyesColorClientService EyesColorClientService { get; set; }
        [Inject]
        private GenderClientService GetGenderClientService { get; set; }
        [Inject]
        private DateObjectiveClientService DateObjectiveClientService { get; set; }
        [Inject]
        private ReligionClientService ReligionClientService { get; set; }
        [Inject]
        private KidStatusClientService KidStatusClientService { get; set; }
        [Inject]
        private TattooStatusClientService TattooStatusClientService { get; set; }

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
            this.AllGenders = await this.GetGenderClientService.GetAllGenderAsync();
            this.AllDateObjectives = await this.DateObjectiveClientService.GetAllDateObjectiveAsync();
            this.AllReligions = await this.ReligionClientService.GetAllReligionAsync();
            this.AllKidStatus = await this.KidStatusClientService.GetAllKidStatusAsync();
            this.AllTatooStatus = await this.TattooStatusClientService.GetAllTattooStatusAsync();
        }

        private async Task UpdateMyUserProfileAsync()
        {
            await Task.Yield();
            //await this.UserProfileClientService.UpdateMyUserProfileAsync(this.MyUserProfileModel);
        }
    }
}
