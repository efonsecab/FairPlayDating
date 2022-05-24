using FairPlayDating.Common.Interfaces;
using FairPlayDating.DataAccess.Data;
using FairPlayDating.DataAccess.Models;
using FairPlayDating.Models.CustomHttpResponse;
using FairPlayDating.Server.CustomProviders;
using FairPlayDating.Server.Swagger.Filters;
using FairPlayDating.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FairPlayDating.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));
            services.Configure<MicrosoftIdentityOptions>(JwtBearerDefaults.AuthenticationScheme, options => 
            {
                options.Scope.Add("user_photos");
            });
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "Role";
                options.SaveToken = true;
                options.Events.OnMessageReceived = (context) =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments(Common.Global.Constants.Hubs.NotificationHub)))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                };
                options.Events.OnTokenValidated = async (context) =>
                {
                    FairPlayDatingDbContext fairPlayDatingDbContext = CreateFairPlayDatingDbContext(services);
                    ClaimsIdentity claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    var userObjectIdClaim = claimsIdentity.Claims.Single(p => p.Type == Common.Global.Constants.Claims.ObjectIdentifier);
                    var user = await fairPlayDatingDbContext.ApplicationUser
                    .Include(p => p.ApplicationUserRole)
                    .ThenInclude(p => p.ApplicationRole)
                    .Where(p => p.AzureAdB2cobjectId.ToString() == userObjectIdClaim.Value)
                    .SingleOrDefaultAsync();
                    var fullName = claimsIdentity.FindFirst(Common.Global.Constants.Claims.Name).Value;
                    var emailAddress = claimsIdentity.FindFirst(Common.Global.Constants.Claims.Emails).Value;
                    if (user != null && user.ApplicationUserRole != null)
                    {
                        claimsIdentity.AddClaim(new Claim("Role", user.ApplicationUserRole.ApplicationRole.Name));
                        user.FullName = fullName;
                        user.EmailAddress = emailAddress;
                        user.LastLogIn = DateTimeOffset.UtcNow;
                        await fairPlayDatingDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        if (user == null)
                        {
                            var userRole = await fairPlayDatingDbContext.ApplicationRole.FirstAsync(p => p.Name == Common.Global.Constants.Roles.User);
                            user = new ApplicationUser()
                            {
                                LastLogIn = DateTimeOffset.UtcNow,
                                FullName = fullName,
                                EmailAddress = emailAddress,
                                AzureAdB2cobjectId = Guid.Parse(userObjectIdClaim.Value)
                            };
                            await fairPlayDatingDbContext.ApplicationUser.AddAsync(user);
                            await fairPlayDatingDbContext.SaveChangesAsync();
                            await fairPlayDatingDbContext.ApplicationUserRole.AddAsync(new ApplicationUserRole()
                            {
                                ApplicationUserId = user.ApplicationUserId,
                                ApplicationRoleId = userRole.ApplicationRoleId
                            });
                            await fairPlayDatingDbContext.SaveChangesAsync();
                            claimsIdentity.AddClaim(new Claim("Role", user.ApplicationUserRole.ApplicationRole.Name));
                        }
                    }
                };
            });

            GlobalPackageConfiguration.RapidApiKey = Configuration["RapidApiKey"];
            GlobalPackageConfiguration.EnableHttpRequestInformationLog = false;
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddScoped(serviceProvider =>
            {
                var fairplaydatingDatabaseContext = this.CreateFairPlayDatingDbContext(services);
                return fairplaydatingDatabaseContext;
            });

            services.AddTransient<CustomHttpClientHandler>();
            services.AddTransient<CustomHttpClient>();
            services.AddTransient<FacebookGraphService>( sp=> 
            {
                var httpContextAccesor = sp.GetRequiredService<IHttpContextAccessor>();
                var claims = httpContextAccesor.HttpContext.User.Claims;
                var idpAccessToken = claims.Single(p => p.Type == "idp_access_token").Value;
                FacebookGraphConfiguration facebookGraphConfiguration =
                new FacebookGraphConfiguration()
                {
                    AccessToken = idpAccessToken
                };
                FacebookGraphService facebookGraphService = new FacebookGraphService(null,
                    facebookGraphConfiguration, sp.GetRequiredService<CustomHttpClient>());
                return facebookGraphService;
            });
            services.AddTransient<ActivityService>();
            services.AddTransient<UserActivityService>();
            services.AddTransient<FrequencyService>();
            services.AddTransient<UserProfileService>();
            services.AddTransient<HairColorService>();
            services.AddTransient<EyesColorService>();
            services.AddTransient<GenderService>();
            services.AddTransient<DateObjectiveService>();

            services.AddControllersWithViews();
            services.AddAutoMapper(configAction =>
            {
                configAction.AddMaps(new[] { typeof(Startup).Assembly });
            });
            services.AddRazorPages();

            bool enableSwagger = Convert.ToBoolean(Configuration["EnableSwaggerUI"]);
            if (enableSwagger)
            {
                var azureAdB2CInstance = Configuration["AzureAdB2C:Instance"];
                var azureAdB2CDomain = Configuration["AzureAdB2C:Domain"];
                var azureAdB2CClientAppClientId = Configuration["AzureAdB2C:ClientAppClientId"];
                var azureAdB2ClientAppDefaultScope = Configuration["AzureAdB2C:ClientAppDefaultScope"];
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "FairPlayDating API" });
                    c.CustomSchemaIds(p => p.FullName);
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows()
                        {
                            Implicit = new OpenApiOAuthFlow()
                            {
                                AuthorizationUrl = new Uri($"{azureAdB2CInstance}/{azureAdB2CDomain}/oauth2/v2.0/authorize"),
                                TokenUrl = new Uri($"{azureAdB2CInstance}/{azureAdB2CDomain}/oauth2/v2.0/token"),
                                Scopes = new Dictionary<string, string>
                               {
                               {azureAdB2ClientAppDefaultScope, "Access APIs" }
                               }
                            },
                        }
                    });
                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                });
            }
        }

        private FairPlayDatingDbContext CreateFairPlayDatingDbContext(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var currentUserProvider = sp.GetService<ICurrentUserProvider>();
            return ConfigureFairPlayDatingDataContext(currentUserProvider);
        }

        private FairPlayDatingDbContext ConfigureFairPlayDatingDataContext(ICurrentUserProvider currentUserProvider)
        {
            DbContextOptionsBuilder<FairPlayDatingDbContext> dbContextOptionsBuilder =
                new();
            FairPlayDatingDbContext fairplaydatingDatabaseContext =
            new(dbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"),
            sqlServerOptionsAction: (serverOptions) => serverOptions
            .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null)).Options,
            currentUserProvider);
            return fairplaydatingDatabaseContext;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            bool enableSwagger = Convert.ToBoolean(Configuration["EnableSwaggerUI"]);
            if (enableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FairPlayDating API");
                    c.OAuthClientId(Configuration["AzureAdB2C:ClientAppClientId"]);
                    c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>()
                    {
                    {"p", Configuration["AzureAdB2C:SignUpSignInPolicyId"] }
                    });
                });
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(cfg =>
            {
                cfg.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();
                    var error = exceptionHandlerPathFeature.Error;
                    if (error != null)
                    {
                        try
                        {
                            FairPlayDatingDbContext fairPlayDatingDbContext =
                            this.CreateFairPlayDatingDbContext(context.RequestServices);
                            await fairPlayDatingDbContext.ErrorLog.AddAsync(new ErrorLog()
                            {
                                FullException = error.ToString(),
                                StackTrace = error.StackTrace,
                                Message = error.Message
                            });
                            await fairPlayDatingDbContext.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        ProblemHttpResponse problemHttpResponse = new()
                        {
                            Detail = error.Message,
                        };
                        await context.Response.WriteAsJsonAsync<ProblemHttpResponse>(problemHttpResponse);
                    }
                });
            });

            //app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        private FairPlayDatingDbContext CreateFairPlayDatingDbContext(IServiceProvider serviceProvider)
        {
            var currentUserProvider = serviceProvider.GetService<ICurrentUserProvider>();
            return ConfigureFairPlayDatingDataContext(currentUserProvider);
        }
    }
}
