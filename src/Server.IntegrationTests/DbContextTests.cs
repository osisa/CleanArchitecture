
using System;
using System.Linq;

using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void GetUsers()
        {
            // Arrange
            var webHostBuilder = TestValues.CreateWebHostBuilder();

            // Act
            using (var server = new TestServer(webHostBuilder))
            {
                var blazorHeroContext = server.Host.Services.GetRequiredService<BlazorHeroContext>();
                var users = blazorHeroContext.Users.ToArray();
                
                // Assert
                users.Length.Should().Be(2);

                server.Host.StopAsync().GetAwaiter().GetResult();
            }
        }
    }
}