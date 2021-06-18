﻿using AutoMapper;
using FairPlayDating.Common.Global;
using FairPlayDating.DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Server.Tests
{
    public abstract class TestsBase
    {
        TestServer Server { get; }
        protected IMapper Mapper { get; }
        private static IHttpContextAccessor HttpContextAccessor { get; set; }
        internal static string TestVideoBloblUrl { get; set; }
        internal static TestAzureAdB2CAuthConfiguration TestAzureAdB2CAuthConfiguration { get; set; }
        protected static IConfiguration Configuration { get; set; }
        private HttpClient UserRoleAuthorizedHttpClient { get; set; }
        private HttpClient AdminRoleAuthorizedHttpClient { get; set; }


        public TestsBase()
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddJsonFile("appsettings.json")
                .AddUserSecrets(typeof(TestsBase).Assembly,optional:true);
            var configRoot = configurationBuilder.Build();
            //configurationBuilder.AddAzureAppConfiguration(options =>
            //{
            //    var azureAppConfigConnectionString =
            //        configRoot[Constants.ConfigurationKeysNames.AzureAppConfigConnectionString];
            //    options.Connect(azureAppConfigConnectionString);
            //});
            IConfiguration configuration = configurationBuilder.Build();
            Server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
                {
                    IConfigurationRoot configurationRoot = configurationBuilder.Build();

                    var defaultConnectionString = configurationRoot.GetConnectionString(
                        Common.Global.Constants.ConfigurationKeysNames.DefaultConnectionString);
                    DbContextOptionsBuilder<FairPlayDatingDbContext> dbContextOptionsBuilder = new();

                    using FairPlayDatingDbContext fairPlayDatingDbContext =
                    new(dbContextOptionsBuilder.UseSqlServer(defaultConnectionString,
                    sqlServerOptionsAction: (serverOptions) => serverOptions.EnableRetryOnFailure(3)).Options);

                })
                .UseConfiguration(configuration)
                .UseStartup<Startup>());
            Configuration = Server.Services.GetRequiredService<IConfiguration>();
            this.Mapper = Server.Services.GetRequiredService<IMapper>();
            HttpContextAccessor = Server.Services.GetRequiredService<IHttpContextAccessor>();
            TestAzureAdB2CAuthConfiguration = Configuration.GetSection("TestAzureAdB2CAuthConfiguration").Get<TestAzureAdB2CAuthConfiguration>();
        }

        public static FairPlayDatingDbContext CreateDbContext()
        {
            var connectionString = Configuration.GetConnectionString("Default");
            DbContextOptionsBuilder<FairPlayDatingDbContext> dbContextOptionsBuilder =
            new();
            FairPlayDatingDbContext fairPlayDatingDbContext =
            new(dbContextOptionsBuilder.UseSqlServer(connectionString).Options,
            new CustomProviders.CurrentUserProvider(HttpContextAccessor));
            return fairPlayDatingDbContext;
        }

        public enum Role
        {
            Admin,
            User
        }

        protected HttpClient CreateAnonymousClient()
        {
            return this.Server.CreateClient();
        }

        protected async Task<HttpClient> CreateAuthorizedClientAsync(Role role)
        {

            switch (role)
            {
                case Role.Admin:
                    if (this.AdminRoleAuthorizedHttpClient != null)
                        return this.AdminRoleAuthorizedHttpClient;
                    break;
                case Role.User:
                    if (this.UserRoleAuthorizedHttpClient != null)
                        return this.UserRoleAuthorizedHttpClient;
                    break;
            }
            HttpClient httpClient = new();
            List<KeyValuePair<string?, string?>> formData = new();
            formData.Add(new KeyValuePair<string?, string?>("username",
                role == Role.User ? TestAzureAdB2CAuthConfiguration.UserRoleUsername : TestAzureAdB2CAuthConfiguration.AdminRoleUsername));
            formData.Add(new KeyValuePair<string?, string?>("password",
                role == Role.User ? TestAzureAdB2CAuthConfiguration.UserRolePassword : TestAzureAdB2CAuthConfiguration.AdminRolePassword));
            formData.Add(new KeyValuePair<string?, string?>("grant_type", "password"));
            string applicationId = TestAzureAdB2CAuthConfiguration.ApplicationId;
            formData.Add(new KeyValuePair<string?, string?>("scope", $"openid {applicationId} offline_access"));
            formData.Add(new KeyValuePair<string?, string?>("client_id", applicationId));
            formData.Add(new KeyValuePair<string?, string?>("response_type", "token id_token"));
            System.Net.Http.FormUrlEncodedContent form = new(formData);
            var response = await httpClient.PostAsync(TestAzureAdB2CAuthConfiguration.TokenUrl, form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                var client = this.Server.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Access_token);
                switch (role)
                {
                    case Role.Admin:
                        this.AdminRoleAuthorizedHttpClient = client;
                        break;
                    case Role.User:
                        this.UserRoleAuthorizedHttpClient = client;
                        break;
                }
                return client;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

    }

    public class AuthResponse
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Expires_in { get; set; }
        public string Refresh_token { get; set; }
        public string Id_token { get; set; }
    }

    public class TestAzureAdB2CAuthConfiguration
    {
        public string TokenUrl { get; set; }
        public string UserRoleUsername { get; set; }
        public string UserRolePassword { get; set; }
        public string AdminRoleUsername { get; set; }
        public string AdminRolePassword { get; set; }
        public string ApplicationId { get; set; }
        public string AzureAdUserObjectId { get; set; }
    }
}
