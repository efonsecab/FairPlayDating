using FairPlayDating.MauiBlazor.Features.LogOn;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.MauiBlazor.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public CustomAuthStateProvider()
        {
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new ClaimsIdentity();
            if (UserState.UserContext.IsLoggedOn)
            {
                identity.AddClaim(new Claim("oid", UserState.UserContext.UserIdentifier));
            }
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
