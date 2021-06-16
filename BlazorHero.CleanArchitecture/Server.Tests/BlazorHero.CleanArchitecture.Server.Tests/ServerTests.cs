using System;
using System.Linq;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class ServerTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void Configuration_ForWebHostBuilder_ShouldNotBeNull()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();
            var services = webHostBuilder.Build().Services;

            // Act
            var result = services.GetService<IConfiguration>();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateWebHostBuilder_ShouldReturnNotNullServices()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();

            // Act
            var result = hostBuilder.Build().Services;

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetAll_ForWebHostBuilder_ShouldNotBeNull()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            var userService = services.GetRequiredService<IUserService>();

            // Act
            var result = userService.GetAllAsync().Result.Data;

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [TestMethod]
        public void IUserService_ForWebHostBuilder_ShouldNotBeNull()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

            // Act
            var result = services.GetService<IUserService>();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void Services_ForProgramCreatedHostBuilder_ShouldNotBeNull()
        {
            // Arrange
            var args = Array.Empty<string>();
            var hostBuilder = Program.CreateHostBuilder(args);
            var host = hostBuilder.Build();

            // Act
            var result = host.Services;

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void UserManager_BlazorHeroUser_ForWebHostBuilder_ShouldNotBeNull()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

            // Act
            var result = services.GetService<UserManager<BlazorHeroUser>>();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void UserManager_Users_ForWebHostBuilder_ShouldNotBeNull()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            var userManager = services.GetRequiredService<UserManager<BlazorHeroUser>>();

            // Act
            var result = userManager.Users.ToArray();

            // Assert
            result.Should().NotBeNull();
            result.Length.Should().Be(2);
        }

        #endregion

        #region Methods

        private static IWebHostBuilder CreateWebHostBuilder(string environment = Environments.Development)
        {
            return
                new WebHostBuilder()
                    // PowerShell: set $env:ASPNETCORE_ENVIRONMENT = 'Development'
                    .UseEnvironment(environment) // You can set the environment you want (development, staging, production)
                    .UseConfiguration(
                        new ConfigurationBuilder()
                            // ReSharper disable once StringLiteralTypo
                            .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
                            .Build()
                    )
                    .UseStartup<Startup>(); // Startup class of your web app project
        }

        #endregion
    }
}