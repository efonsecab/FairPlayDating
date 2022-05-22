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
            string fairPlayTubeapiAddress = builder.Configuration["ApiBaseUrl"];
            B2CConstants b2CConstants = builder.Configuration.GetSection("B2CConstants").Get<B2CConstants>();
            builder.Services.AddSingleton(b2CConstants);

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}