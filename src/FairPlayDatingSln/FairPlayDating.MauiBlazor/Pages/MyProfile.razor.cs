using FairPlayDating.ClientServices;
using FairPlayDating.Models.Activity;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Pages
{
    public partial class MyProfile
    {
        private ActivityModel[] AllActivities;

        [Inject]
        private ActivityClientService ActivityClientService { get; set; }

        override protected async Task OnInitializedAsync()
        {
            this.AllActivities = await ActivityClientService.GetAllActivityAsync();
        }
    }
}
