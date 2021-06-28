﻿using System;
using System.Linq;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Infrastructure;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Serilog;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests
{
    [TestClass]
    public class DbContextTests
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the test context.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void Configuration()
        {
            // Arrange
            var webHostBuilder = TestValues.CreateWebHostBuilder();
            var services = webHostBuilder.Build().Services;

            // Act
            var result = services.GetService<IConfiguration>();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateWebHostBuilder()
        {
            // Act
            var result = TestValues.CreateWebHostBuilder();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetDefaultConnectionString()
        {
            // Arrange
            var webHostBuilder = TestValues.CreateWebHostBuilder();
            var services = webHostBuilder.Build().Services;
            var configuration = services.GetRequiredService<IConfiguration>();

            // Act
            var result = configuration.GetConnectionString("DefaultConnection");

            // Assert
            result.Should().NotBeNull();
            TestContext.WriteLine("DefaultConnection: {0}", result);
        }

        [TestMethod]
        public void Constructor()
        {
            // Act
            var result = CreateUnitUnderTest();

            // Assert
            result.Should().NotBeNull();
        }


        [TestMethod]
        public void GetUsers()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.Users.ToArray();

            // Assert
            result.Length.Should().Be(2);
        }

        [TestMethod]
        public void GetUsers2()
        {
            // Arrange
            //var webHostBuilder = TestValues.CreateWebHostBuilder();
            //var host = webHostBuilder.Build();

            // Act
            //var users = CreateUnitUnderTest2();
            var args = new string[] { };

            var hostBuilder= Host.CreateDefaultBuilder(args)
                    .UseSerilog()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStaticWebAssets();
                        webBuilder.UseStartup<TestStartup2>();
                    });

            var host = hostBuilder.Build();

            BlazorHeroUser[] users;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<BlazorHeroContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    var configuration = host.Services.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    TestContext.WriteLine("Connection: {0}", connectionString);

                    host.Start();
                   
                    //// Assert
                    users = context.Users.ToArray();
                    users.Length.Should().Be(2);

                    host.StopAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }


            //var configuration = host.Services.GetRequiredService<IConfiguration>();
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //TestContext.WriteLine("Connection: {0}", connectionString);

            ////host.Run();
            ////var services = host.Services;
            
            //var context2 = host.Services.GetRequiredService<BlazorHeroContext>();

            ////using (var scope = host.Services.CreateScope())
            ////{
            ////    var services = scope.ServiceProvider;

            ////    try
            ////    {
            ////        var context = services.GetRequiredService<BlazorHeroContext>();

            ////        //if (context.Database.IsSqlServer())
            ////        //{
            ////        //    context.Database.Migrate();
            ////        //}

            ////        users = context.Users.ToArray();
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            ////        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            ////        throw;
            ////    }
            ////}


            //// Assert
            //var users = context2.Users.ToArray();
            users.Length.Should().Be(2);

            //return null;

        }
        private static BlazorHeroContext CreateUnitUnderTest()
        {
            var webHostBuilder = TestValues.CreateWebHostBuilder();
            var host = webHostBuilder.Build();

            //BlazorHeroUser[] users;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<BlazorHeroContext>();

                    //if (context.Database.IsSqlServer())
                    //{
                    //    context.Database.Migrate();
                    //}

                    //users = context.Users.ToArray();

                    return context;
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    try
            //    {
            //        var context = services.GetRequiredService<BlazorHeroContext>();

            //        if (context.Database.IsSqlServer())
            //        {
            //            context.Database.Migrate();
            //        }

            //        return context;
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            //        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            //        throw;
            //    }
            //}


            //var context = webHostBuilder.Build().Services.GetRequiredService<BlazorHeroContext>();

            //var environment = "Development";
            //webHostBuilder // You can set the environment you want (development, staging, production)
            //    .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
            //    .UseConfiguration(
            //        new ConfigurationBuilder()
            //            .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
            //            .Build()
            //    );


            //var services = webHostBuilder.Build().Services;
            //var configuration = services.GetRequiredService<IConfiguration>();
            //var connectionString = configuration.GetConnectionString("DefaultConnection");

            //var optionBuilder = new DbContextOptionsBuilder<BlazorHeroContext>();
            //optionBuilder.UseSqlServer(connectionString);


            //var currentUserService = new Mock<ICurrentUserService>().Object;
            //var dateTimeService = new Mock<IDateTimeService>().Object;



            //var context= new BlazorHeroContext(optionBuilder.Options, currentUserService, dateTimeService);
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var seeder = services.GetService<IDatabaseSeeder>();
            //seeder.Initialize();


        }


        private static BlazorHeroUser[] CreateUnitUnderTest2()
        {
            var webHostBuilder = TestValues.CreateWebHostBuilder();
            //var host = webHostBuilder.Build();

            var environment = "Development";
            webHostBuilder

                .UseEnvironment(environment) // You can set the environment you want (development, staging, production)
                //.UseEnvironment("Development") // You can set the environment you want (development, staging, production)
                .UseConfiguration(
                    new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
                        .Build()
                )
                .UseStartup<TestStartup2>();


            var host = webHostBuilder.Build();
            host.Run();


            var context=host.Services.GetRequiredService<BlazorHeroContext>();

            return context.Users.ToArray();
            //.ConfigureServices(s => s.AddTestUser(DefaultUser))
            //.ConfigureServices(s => s.AddSingleton<IAuthenticationHandler, TestAuthenticationHandler>()) // Startup class of your web app project
            //.ConfigureServices(s => s.AddHttpContextAccessor());

            //BlazorHeroUser[] users;
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    try
            //    {
            //        var context = services.GetRequiredService<BlazorHeroContext>();

            //        if (context.Database.IsSqlServer())
            //        {
            //            context.Database.Migrate();
            //        }

            //        users = context.Users.ToArray();

            //        return context.Users.ToArray();
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            //        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            //        throw;
            //    }
            //}

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    try
            //    {
            //        var context = services.GetRequiredService<BlazorHeroContext>();

            //        if (context.Database.IsSqlServer())
            //        {
            //            context.Database.Migrate();
            //        }

            //        return context;
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            //        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            //        throw;
            //    }
            //}


            //var context = webHostBuilder.Build().Services.GetRequiredService<BlazorHeroContext>();

            //var environment = "Development";
            //webHostBuilder // You can set the environment you want (development, staging, production)
            //    .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
            //    .UseConfiguration(
            //        new ConfigurationBuilder()
            //            .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
            //            .Build()
            //    );


            //var services = webHostBuilder.Build().Services;
            //var configuration = services.GetRequiredService<IConfiguration>();
            //var connectionString = configuration.GetConnectionString("DefaultConnection");

            //var optionBuilder = new DbContextOptionsBuilder<BlazorHeroContext>();
            //optionBuilder.UseSqlServer(connectionString);


            //var currentUserService = new Mock<ICurrentUserService>().Object;
            //var dateTimeService = new Mock<IDateTimeService>().Object;



            //var context= new BlazorHeroContext(optionBuilder.Options, currentUserService, dateTimeService);
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var seeder = services.GetService<IDatabaseSeeder>();
            //seeder.Initialize();


        }

        #endregion
    }
}