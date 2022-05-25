using Blazored.Toast.Services;
using FairPlayDating.Common.Global;
using FairPlayDating.MauiBlazor.Features.LogOn;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }
        [Inject]
        private B2CConstants B2CConstants { get; set; }
        public bool HasInternet { get; private set; }

        protected override void OnInitialized()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            switch (Connectivity.NetworkAccess)
            {
                case NetworkAccess.None:
                    HasInternet = false;
                    break;
                case NetworkAccess.Internet:
                    HasInternet = true;
                    break;
            }
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            switch (e.NetworkAccess)
            {
                case NetworkAccess.Internet:
                    if (!UserState.UserContext.IsLoggedOn)
                    {
                        ToastService.ShowInfo("Internet Connection Restored");
                        HasInternet = true;
                        StateHasChanged();
                    }
                    break;
                default:
                    ToastService.ShowWarning("No Internet Connection");
                    HasInternet = false;
                    break;
            }
        }

        private void OnMyProfileButtonClicked()
        {
            this.NavigationManager.NavigateTo(Constants.MauiBlazorAppRoutes.MyUserProfile);
        }

        private async void OnLogoutButtonClicked()
        {
            UserState.UserContext = new UserContext();
            var allAccounts = await B2CConstants.PublicClientApp.GetAccountsAsync();
            foreach (var singleAccount in allAccounts)
            {
                await B2CConstants.PublicClientApp.RemoveAsync(singleAccount);
            }
        }

        private void OnLoginSuccess()
        {
            StateHasChanged();
        }
    }
}
