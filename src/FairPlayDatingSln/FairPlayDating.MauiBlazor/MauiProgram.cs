using FairPlayDating.ClientServices;
using FairPlayDating.Common.Global;
using FairPlayDating.MauiBlazor.Authentication;
using FairPlayDating.MauiBlazor.Data;
using FairPlayDating.MauiBlazor.Features.LogOn;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FairPlayDating.MauiBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string strAppConfigStreamName = string.Empty;
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            strAppConfigStreamName = $"{Constants.Names.ApplicationName}.MauiBlazor.appsettings.Development.json";
#else
            strAppConfigStreamName = $"{Constants.Names.ApplicationName}.MauiBlazor.appsettings.json";
#endif

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MauiProgram)).Assembly;
            var stream = assembly.GetManifestResourceStream(strAppConfigStreamName);
            builder.Configuration.AddJsonStream(stream);
            var services = builder.Services;
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            string assemblyName = Constants.Names.ApplicationName;
            string fairPlayDatingIpAddress = builder.Configuration["ApiBaseUrl"];
            B2CConstants b2CConstants = builder.Configuration.GetSection("B2CConstants").Get<B2CConstants>();
            builder.Services.AddSingleton(b2CConstants);
            builder.Services.AddSingleton<WeatherForecastService>();

            services.AddScoped<BaseAddressAuthorizationMessageHandler>();
            services.AddHttpClient($"{assemblyName}.ServerAPI", client =>
        client.BaseAddress = new Uri(fairPlayDatingIpAddress))
        //.AddHttpMessageHandler<LocalizationMessageHandler>()
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            services.AddHttpClient($"{assemblyName}.ServerAPI.Anonymous", client =>
                client.BaseAddress = new Uri(fairPlayDatingIpAddress));
          //      .AddHttpMessageHandler<LocalizationMessageHandler>();

            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient($"{assemblyName}.ServerAPI"));

            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient($"{assemblyName}.ServerAPI.Anonymous"));


            services.AddTransient<HttpClientService>();
            services.AddTransient<FacebookClientService>();
            services.AddTransient<ActivityClientService>();
            services.AddTransient<UserActivityClientService>();
            services.AddTransient<FrequencyClientService>();
            services.AddTransient<UserProfileClientService>();
            services.AddTransient<HairColorClientService>();
            services.AddTransient<EyesColorClientService>();
            services.AddTransient<GenderClientService>();

            return builder.Build();
        }
    }

    public class BaseAddressAuthorizationMessageHandler : DelegatingHandler
    {
        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AddAuthToken(request);
            return base.Send(request, cancellationToken);
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AddAuthToken(request);
            return await base.SendAsync(request, cancellationToken);
        }

        private void AddAuthToken(HttpRequestMessage request)
        {
            request.Headers.Authorization = new System.Net.Http.Headers
                .AuthenticationHeaderValue("bearer", UserState.UserContext.AccessToken);
        }
    }    
}