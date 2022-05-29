using Blazored.Toast.Services;
using FairPlayDating.ClientServices;
using FairPlayDating.Common.Global;
using FairPlayDating.Models.Match;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Pages
{
    [Route(Constants.MauiBlazorAppRoutes.MyMatches)]
    [Authorize]
    public partial class MyMatches
    {
        [Inject]
        private MatchClientService MatchClientService { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }
        private MatchModel[] AllMyMatches { get; set; }
        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.AllMyMatches = await this.MatchClientService.GetMyMatchesAsync();
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
