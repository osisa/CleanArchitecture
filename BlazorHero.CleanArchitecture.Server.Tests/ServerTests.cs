// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="ServerTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Linq;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Application.Models.Identity;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class ServerTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void CreateHost()
        {
            // arrange
            var args = new string[] { };
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseStaticWebAssets();
                        webBuilder.UseStartup<Startup>();
                    });

            var services = hostBuilder.Build().Services;

            // act
            //var result = services.GetService<IUserManager>();
            var result = services.GetService<IConfiguration>();

            // assert
            result.Should().NotBeNull();
        }
        
        [TestMethod]
        public void UserManager()
        {
            // arrange
            var hostBuilder = CreateWebHost();
            var services = hostBuilder.Build().Services;
            
            // act
            var result = services.GetService<UserManager<BlazorHeroUser>>();

            // assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void UserManagerUsersArrayLength()
        {
            // arrange
            var hostBuilder = CreateWebHost();
            var services = hostBuilder.Build().Services;
            var userManager= services.GetRequiredService<UserManager<BlazorHeroUser>>();

            // act
            var result = userManager.Users.ToArray();

            // assert
            result.Should().NotBeNull();
            result.Length.Should().Be(2);
        }

        [TestMethod]
        public void GetAllUsersAsync()
        {
            // arrange
            var hostBuilder = CreateWebHost();
            var services = hostBuilder.Build().Services;
            var userService = services.GetRequiredService<IUserService>();

            // act
            var result = userService.GetAllAsync().Result.Data;

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [TestMethod]
        public void Run()
        {
            // Arrange
            var args = new string[] { };
            var hostBuilder = Program.CreateHostBuilder(args);
            var host = hostBuilder.Build();

            // Act
            var result = host.Services;
            
            // Assert
            result.Should().NotBeNull();
        }

        //[TestMethod]
        //public void Run2()
        //{
        //    // Arrange
        //    var args = new string[] { };
        //    var hostBuilder = Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(
        //            webBuilder =>
        //            {
        //                webBuilder.UseStaticWebAssets();
        //                webBuilder.UseStartup<Startup>();
        //            });
        //    var host = hostBuilder.Build();

        //    // Act
        //    var result = host.Services.GetService<IUserService>();

        //    // Assert
        //    result.Should().NotBeNull();
        //}

        [TestMethod]
        public void UserService()
        {
            // Arrange
            var host = CreateWebHost().Build();
            
            // Act
            var result = host.Services.GetService<IUserService>();

            // Assert
            result.Should().NotBeNull();
        }

        //[TestMethod]
        //public void Main()
        //{
        //    // Arrange
        //    var args = new string[] { };

        //    // Act
        //    Action result=()=>Program.Main(args);

        //    // Assert
        //    result.Should().NotThrow();
        //}

        //[TestMethod]
        //public void TestServerRun()
        //{
        //    // arrange
        //    var webHostBuilder = CreateWebHost();

        //    //new WebHostBuilder()
        //    //    //.UseEnvironment("Test") // You can set the environment you want (development, staging, production)
        //    //    .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
        //    //    .UseConfiguration(new ConfigurationBuilder()
        //    //        .AddJsonFile("appsettings.json") //the file is set to be copied to the output directory if newer
        //    //        .Build()
        //    //    )
        //    //    .UseStartup<Startup>(); // Startup class of your web app project

        //    using (var server = new TestServer(webHostBuilder))
        //    {
        //        using (var client = server.CreateClient())
        //        {
        //            // act
        //            string result = client.GetStringAsync("/Login").Result;

        //            // assert
        //            result.Should().Contain("temperatureF");
        //        }
        //    }
        //}

        [TestMethod]
        public void EnsureConfig()
        {
            // arrange
            var args = new string[] { };
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseStaticWebAssets();
                        webBuilder.UseStartup<Startup>();
                    });
            var host= hostBuilder.Build();
            
            // act
            var result = host.Services.GetRequiredService<IConfiguration>();

            // assert
            result.Should().NotBeNull();
        }
        
        [TestMethod]
        public void EnsureConfig2()
        {
            // arrange
            //var webHostBuilder =
            //    new WebHostBuilder()
            //        //.UseEnvironment("Test") // You can set the environment you want (development, staging, production)
            //        .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
            //        .UseConfiguration(new ConfigurationBuilder()
            //            .AddJsonFile("appsettings.json") //the file is set to be copied to the output directory if newer
            //            .Build()
            //        )
            //        .UseStartup<Startup>(); // Startup class of your web app project

            var host = CreateWebHost().Build();
            var services = host.Services;

            // act
            var result = services.GetService<IConfiguration>();

            // assert
            result.Should().NotBeNull();

        }
        
        private static IWebHostBuilder CreateWebHost()
        {
            return
                new WebHostBuilder()
                    //.UseEnvironment("Test") // You can set the environment you want (development, staging, production)
                    .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
                    .UseConfiguration(new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json") //the file is set to be copied to the output directory if newer
                        .Build()
                    )
                    .UseStartup<Startup>(); // Startup class of your web app project

        }

        #endregion
    }
}