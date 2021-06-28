﻿//////// --------------------------------------------------------------------------------------------------------------------
//////// <copyright company="o.s.i.s.a. GmbH" file="ServerTests.cs">
////////    (c) 2014. See licence text in binary folder.
//////// </copyright>
////////  --------------------------------------------------------------------------------------------------------------------

////////using BlazorHero.CleanArchitecture.Application.Models.Identity;

//////using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
//////using BlazorHero.CleanArchitecture.Server;

//////using FluentAssertions;

//////using Microsoft.AspNetCore.Hosting;
//////using Microsoft.AspNetCore.Identity;
//////using Microsoft.Extensions.Configuration;
//////using Microsoft.Extensions.DependencyInjection;
//////using Microsoft.VisualStudio.TestTools.UnitTesting;

//////namespace Server.Tests
//////{
//////    [TestClass]
//////    public class TokenTests
//////    {
//////        #region Public Methods and Operators
        
//////        [TestMethod]
//////        public void UserManager()
//////        {
//////            // arrange
//////            var hostBuilder = CreateWebHost();
//////            var services = hostBuilder.Build().Services;
            
//////            // act
//////            var result = services.GetService<UserManager<BlazorHeroUser>>();

//////            // assert
//////            result.Should().NotBeNull();
//////        }
        
//////        private static IWebHostBuilder CreateWebHost()
//////        {
//////            return
//////                new WebHostBuilder()
//////                    //.UseEnvironment("Test") // You can set the environment you want (development, staging, production)
//////                    .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
//////                    .UseConfiguration(new ConfigurationBuilder()
//////                        .AddJsonFile("appsettings.json") //the file is set to be copied to the output directory if newer
//////                        .Build()
//////                    )
//////                    .UseStartup<Startup>(); // Startup class of your web app project
//////        }

//////        #endregion
//////    }
//////}