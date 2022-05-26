using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Services;
using System.Text.Json.Serialization;

namespace ContentModeration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            GlobalPackageConfiguration.RapidApiKey = builder.Configuration["RapidApiKey"];
            GlobalPackageConfiguration.EnableHttpRequestInformationLog = false;
            builder.Services.AddTransient<CustomHttpClientHandler>();
            builder.Services.AddTransient<CustomHttpClient>();

            ConfigureAzureContentModerator(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureAzureContentModerator(WebApplicationBuilder builder)
        {
            AzureContentModeratorConfiguration azureContentModeratorConfiguration =
                            builder.Configuration.GetSection(nameof(AzureContentModeratorConfiguration))
                            .Get<AzureContentModeratorConfiguration>();
            builder.Services.AddSingleton(azureContentModeratorConfiguration);
            builder.Services.AddTransient<AzureContentModeratorService>();
        }
    }
}