using FairPlayDating.ClientServices;
using FairPlayDating.Models.Match;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Pages
{
    public partial class MyMatches
    {
        public MatchModel[] AllMyMatches { get; private set; }
        [Inject]
        private MatchClientService MatchClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.AllMyMatches = await this.MatchClientService.GetMyMatchesAsync();
        }
    }
}
