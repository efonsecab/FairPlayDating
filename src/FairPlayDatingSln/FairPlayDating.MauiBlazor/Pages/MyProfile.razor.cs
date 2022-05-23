using FairPlayDating.ClientServices;
using FairPlayDating.MauiBlazor.ExtendedModels;
using FairPlayDating.Models.Activity;
using FairPlayDating.Models.Frequency;
using FairPlayDating.Models.UserActivity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Pages
{
    [Authorize]
    public partial class MyProfile
    {
        private ActivityModel[] AllActivities;

        public FrequencyModel[] AllFrequencies { get; private set; }
        public ExtendedUserActivityModel[] ExtendedUserActivities { get; private set; }
        [Inject]
        private ActivityClientService ActivityClientService { get; set; }
        [Inject]
        private FrequencyClientService FrequencyClientService { get; set; }
        [Inject]
        private UserActivityClientService UserActivityClientService { get; set; }

        override protected async Task OnInitializedAsync()
        {
            this.AllActivities = await ActivityClientService.GetAllActivityAsync();
            this.AllFrequencies = await this.FrequencyClientService.GetAllFrequencyAsync();
            this.ExtendedUserActivities = this.AllActivities
                .Select(p => new ExtendedUserActivityModel()
                {
                    ActivityId = p.ActivityId,
                    FrequencyId = 1,
                    ActivityName = p.Name,
                    FrequencyName = this.AllFrequencies.Single(p => p.FrequencyId == 1).Name
                })
                .ToArray();
            var allMyUserActivities = await this.UserActivityClientService.GetMyUserActivitiesAsync();
            if (allMyUserActivities != null)
            {
                foreach (var userActivity in allMyUserActivities)
                {
                    var extendedUserActivity = this.ExtendedUserActivities.Single(p => p.ActivityId == userActivity.ActivityId);
                    extendedUserActivity.FrequencyId = userActivity.FrequencyId;
                    extendedUserActivity.FrequencyName = this.AllFrequencies.Single(p => p.FrequencyId == userActivity.FrequencyId).Name;
                }
            }
        }

        private async Task OnExtendedActivityFrequencyChangedAsync(
            ExtendedUserActivityModel extendedUserActivityModel, ChangeEventArgs e)
        {
            extendedUserActivityModel.FrequencyId = Convert.ToInt16(e.Value);
            extendedUserActivityModel.FrequencyName =
                this.AllFrequencies
                .Single(p => p.FrequencyId == extendedUserActivityModel.FrequencyId).Name;
            await this.UserActivityClientService.UpdateMyUserActivityAsync(extendedUserActivityModel);
        }
    }
}
