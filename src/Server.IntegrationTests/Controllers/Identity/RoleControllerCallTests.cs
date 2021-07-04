// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="UserControllerCallTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Identity
{
    [TestClass]
    public class RoleControllerCallTests : TestBase
    {
        private const string BaseAddress = "api/identity/role";

        #region Public Methods and Operators

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<List<RoleResponse>>>($"{BaseAddress}");

            // Assert
            result.Should().NotBeNull();
            result.Data.Count.Should().BeGreaterThan(0);
            TestContext.WriteLine("");
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}", RoleControllerValues.NewRoleRequest);

            // Assert
            result.EnsureSuccessStatusCode();
            
            var text = result.Content.ToResult<string>();
            TestContext.WriteLine("Text:{0}", text.Messages[0]);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            client.PostAsync($"{BaseAddress}", RoleControllerValues.NewRoleRequest);

            var blazorHeroContext = server.Services.GetRequiredService<BlazorHeroContext>();
            var role = blazorHeroContext.Roles.SingleAsync(r => r.Name == RoleControllerValues.NewRoleRequest.Name).Result;
            
            // Act
            var result = client.DeleteAsync($"{BaseAddress}/{role.Id}").Result;

            // Assert
            result.EnsureSuccessStatusCode();
        }
        
        #endregion
    }
}