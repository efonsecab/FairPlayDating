using AutoMapper;
using FairPlayDating.ClientServices;
using FairPlayDating.Common.CustomExceptions;
using FairPlayDating.Common.Global;
using FairPlayDating.DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
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
        public TestsHttpClientFactory TestsHttpClientFactory { get; private set; } = new();
        public static TestServer? Server { get; private set; }
        private HttpClient? UserRoleAuthorizedHttpClient { get; set; }
        private HttpClient? AdminRoleAuthorizedHttpClient { get; set; }
        internal static TestAzureAdB2CAuthConfiguration? TestAzureAdB2CAuthConfiguration { get; set; }
        internal static string? UserBearerToken { get; set; }
        public TestsBase()
        {

            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .AddUserSecrets<TestsBase>();
            var configRoot = configurationBuilder.Build();
            IConfiguration configuration = configurationBuilder.Build();
            var application = new WebApplicationFactory<Program>()
        //.WithWebHostBuilder(builder =>
        //{
        //    builder.ConfigureServices(services => 
        //    {
        //        Startup.ConfigureServices(services, configuration);
        //    });
        //    // ... Configure test services
        //})
        ;

            TestAzureAdB2CAuthConfiguration = configRoot
                .GetSection(nameof(TestAzureAdB2CAuthConfiguration)).Get<TestAzureAdB2CAuthConfiguration>();
            Server = application.Server;
        }

        private HttpClientService CreateHttpClientService()
        {
            HttpClientService httpClientService = new(this.TestsHttpClientFactory);
            return httpClientService;
        }

        protected UserPhotoClientService CreateUserPhotoClientService()
        {
            UserPhotoClientService userPhotoClientService = new(CreateHttpClientService());
            return userPhotoClientService;
        }

        protected async Task<HttpClient> SignIn(Role role)
        {
            var authorizedHttpClient = await CreateAuthorizedClientAsync(role);
            _ = await authorizedHttpClient.GetStringAsync(Constants.ApiRoutes.ApplicationUserController.GetMyRoles);
            return authorizedHttpClient;
        }

        public enum Role
        {
            Admin,
            User
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
                role == Role.User ? TestAzureAdB2CAuthConfiguration!.UserRoleUsername : TestAzureAdB2CAuthConfiguration!.AdminRoleUsername));
            formData.Add(new KeyValuePair<string?, string?>("password",
                role == Role.User ? TestAzureAdB2CAuthConfiguration.UserRolePassword : TestAzureAdB2CAuthConfiguration.AdminRolePassword));
            formData.Add(new KeyValuePair<string?, string?>("grant_type", "password"));
            string? applicationId = TestAzureAdB2CAuthConfiguration.ApplicationId;
            formData.Add(new KeyValuePair<string?, string?>("scope", $"openid {applicationId} offline_access"));
            formData.Add(new KeyValuePair<string?, string?>("client_id", applicationId));
            formData.Add(new KeyValuePair<string?, string?>("response_type", "token id_token"));
            System.Net.Http.FormUrlEncodedContent form = new(formData);
            var response = await httpClient.PostAsync(TestAzureAdB2CAuthConfiguration.TokenUrl, form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                var client = Server!.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result!.Access_token);
                switch (role)
                {
                    case Role.Admin:
                        this.AdminRoleAuthorizedHttpClient = client;
                        break;
                    case Role.User:
                        this.UserRoleAuthorizedHttpClient = client;
                        break;
                }
                UserBearerToken = result!.Access_token;
                return client;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new CustomValidationException(error);
            }
        }
    }

    public class TestsHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            string assemblyName = Constants.Names.ApplicationName;
            var serverApiClientName = $"{assemblyName}.ServerAPI";
            var client = TestsBase.Server!.CreateClient();
            //if (name == serverApiClientName)
            //{
            //    client.DefaultRequestHeaders.Authorization =
            //        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TestsBase.UserBearerToken);
            //    return client;
            //}
            //else
            return client;
        }
    }

    public class AuthResponse
    {
        public string? Access_token { get; set; }
        public string? Token_type { get; set; }
        public string? Expires_in { get; set; }
        public string? Refresh_token { get; set; }
        public string? Id_token { get; set; }
    }

    public class TestAzureAdB2CAuthConfiguration
    {
        public string? TokenUrl { get; set; }
        public string? UserRoleUsername { get; set; }
        public string? UserRolePassword { get; set; }
        public string? AdminRoleUsername { get; set; }
        public string? AdminRolePassword { get; set; }
        public string? ApplicationId { get; set; }
        public string? AzureAdUserObjectId { get; set; }
    }
}
