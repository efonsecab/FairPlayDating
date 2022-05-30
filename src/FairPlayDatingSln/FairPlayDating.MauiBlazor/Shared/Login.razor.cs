using Blazored.Toast.Services;
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
    public partial class Login
    {
        [Parameter]
        [EditorRequired]
        public EventCallback OnLoginSuccess { get; set; }
        [Inject]
        private B2CConstants B2CConstants { get; set; }

        private async Task Authenticate()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await B2CConstants.PublicClientApp.GetAccountsAsync();
            try
            {
                IAccount currentUserAccount = GetAccountByPolicy(accounts, B2CConstants.PolicySignUpSignIn);
                authResult = await B2CConstants.PublicClientApp
                    .AcquireTokenSilent(B2CConstants.ApiScopesArray, currentUserAccount)
                    .ExecuteAsync();

                UserState.UserContext = new UserContext()
                {
                    AccessToken = authResult.AccessToken,
                    IsLoggedOn = true,
                    UserIdentifier = authResult.UniqueId,
                    Idp_Access_Token = authResult.ClaimsPrincipal.Claims.Single(p => p.Type == "idp_access_token").Value
                };
                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
                await OnLoginSuccess.InvokeAsync();
            }
            catch (MsalUiRequiredException)
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
                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
                await OnLoginSuccess.InvokeAsync();
            }

            catch (Exception)
            {
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
    }
}
