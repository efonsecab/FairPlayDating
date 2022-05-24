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
        private B2CConstants B2CConstants { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        public bool HasInternet { get; private set; }

        protected override async Task OnInitializedAsync()
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
            if (this.HasInternet && !UserState.UserContext.IsLoggedOn)
            {
                await Login();
            }
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            switch (e.NetworkAccess)
            {
                case NetworkAccess.Internet:
                    if (!UserState.UserContext.IsLoggedOn)
                    {
                        await Login();
                        HasInternet = true;
                        StateHasChanged();
                    }
                    break;
                default:
                    HasInternet = false;
                    break;
            }
        }

        private async Task Login()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await B2CConstants.PublicClientApp.GetAccountsAsync();
            try
            {
                IAccount currentUserAccount = GetAccountByPolicy(accounts, B2CConstants.PolicySignUpSignIn);
                authResult = await B2CConstants.PublicClientApp
                    .AcquireTokenSilent(B2CConstants.ApiScopesArray, currentUserAccount)
                    .ExecuteAsync();

                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
            }
            catch (MsalUiRequiredException ex)
            {
                authResult = await B2CConstants.PublicClientApp
                    .AcquireTokenInteractive(B2CConstants.ApiScopesArray)
#if ANDROID
                    .WithParentActivityOrWindow(Platform.CurrentActivity)
#endif
                    .WithAccount(GetAccountByPolicy(accounts, B2CConstants.PolicySignUpSignIn))
                    .WithPrompt(Prompt.SelectAccount)
                    .ExecuteAsync();
                UserState.UserContext = new UserContext()
                {
                    AccessToken = authResult.AccessToken,
                    IsLoggedOn = true,
                    UserIdentifier = authResult.UniqueId,
                    Idp_Access_Token = authResult.ClaimsPrincipal.Claims.Single(p => p.Type == "idp_access_token").Value
                };
                //NavigationManager.NavigateTo("/", true);
                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
            }

            catch (Exception ex)
            {
                string message = $"Users:{string.Join(",", accounts.Select(u => u.Username))}{Environment.NewLine}Error Acquiring Token:{Environment.NewLine}{ex}";
                //await ToastifyService.DisplayErrorNotification(message);
            }
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }

        private void DisplayBasicTokenInfo(AuthenticationResult authResult)
        {
            //TokenInfoText.Text = "";
            //if (authResult != null)
            //{
            //    TokenInfoText.Text += $"Name: {authResult.Account.Username}" + Environment.NewLine;
            //    TokenInfoText.Text += $"Token Expires: {authResult.ExpiresOn.ToLocalTime()}" + Environment.NewLine;
            //    TokenInfoText.Text += $"Id Token: {authResult.IdToken}" + Environment.NewLine;
            //    TokenInfoText.Text += $"Tenant Id: {authResult.TenantId}" + Environment.NewLine;
            //}
        }

        private void UpdateSignInState(bool signedIn)
        {
            if (signedIn)
            {
                //CallApiButton.Visibility = Visibility.Visible;
                //EditProfileButton.Visibility = Visibility.Visible;
                //SignOutButton.Visibility = Visibility.Visible;

                //SignInButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                //ResultText.Text = "";
                //TokenInfoText.Text = "";

                //CallApiButton.Visibility = Visibility.Collapsed;
                //EditProfileButton.Visibility = Visibility.Collapsed;
                //SignOutButton.Visibility = Visibility.Collapsed;

                //SignInButton.Visibility = Visibility.Visible;
            }
        }

        private void OnMyProfileButtonClicked()
        {
            this.NavigationManager.NavigateTo(Constants.MauiBlazorAppRoutes.MyUserProfile);
        }
    }
}
