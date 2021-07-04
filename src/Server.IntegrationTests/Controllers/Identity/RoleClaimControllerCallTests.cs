// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="UserControllerCallTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Identity
{
    [TestClass]
    public class RoleClaimControllerCallTests : TestBase
    {
        private const string BaseAddress = "api/identity/roleClaim";

        #region Public Methods and Operators

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<List<RoleClaimResponse>>>($"{BaseAddress}");

            // Assert
            result.Should().NotBeNull();
            result.Data.Count.Should().BeGreaterThan(10);
            TestContext.WriteLine("");
        }
        
        [TestMethod]
        public void GetAllByRoleId()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<List<RoleClaimResponse>>>($"{BaseAddress}/{Id0}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().Be(0);
        }
        
        [TestMethod]
        public void Post()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}", RoleClaimControllerValues.NewRoleClaimRequest);

            // Assert
            result.EnsureSuccessStatusCode();
            
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.DeleteAsync($"{BaseAddress}/{RoleClaimControllerValues.RoleId}").Result;

            // Assert
            result.EnsureSuccessStatusCode();
        }


        #endregion
    }
}