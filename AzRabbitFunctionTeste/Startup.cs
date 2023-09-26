using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registers;
using System.IO;

[assembly: FunctionsStartup(typeof(AzRabbitFunctionTeste.Startup))]
namespace AzRabbitFunctionTeste
{

    public class Startup : FunctionsStartup
    {
        private static IConfiguration _configuration;

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
           .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
           .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"local.settings.json"), optional: true, reloadOnChange: false)
           .AddEnvironmentVariables();

            _configuration = builder.ConfigurationBuilder.Build();

        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            builder.Services.AddFunctionInjections(_configuration);
        }
    }
}
