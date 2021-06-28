using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure
{
    public static class WebHostBuilderHelper
    {
        private static IWebHostBuilder CreateWebHostBuilder(string environment = "Development")
            => new WebHostBuilder()
                .UseEnvironment(environment) // You can set the environment you want (development, staging, production)
                .UseConfiguration(
                    new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
                        .Build()
                )
                .UseStartup<TestStartup>(); // Startup class of your web app project
    }
}
