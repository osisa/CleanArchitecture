using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Server.Controllers.Identity;
using BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure;
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

            client.Post(RoleControllerCallTests.BaseAddress, RoleControllerValues.NewRoleRequest);

            var blazorHeroContext = server.Services.GetRequiredService<BlazorHeroContext>();
            var role = blazorHeroContext.Roles.SingleAsync(r => r.NormalizedName == RoleControllerValues.NewRoleRequest.Name.ToUpperInvariant()).Result;
            
            // Act
            var result = client.Post($"{BaseAddress}", RoleClaimControllerValues.CreateRoleClaimRequest(role.Id));

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


            var blazorHeroContext = server.Services.GetRequiredService<BlazorHeroContext>();
            var roleClaim = blazorHeroContext.RoleClaims.SingleAsync(r => r.ClaimValue == "TestClaimValue").Result;

            //client.Post($"{BaseAddress}", RoleClaimControllerValues.NewRoleClaimRequest).EnsureSuccessStatusCode();

            //client.Post($"{RoleControllerCallTests.BaseAddress}", RoleControllerValues.NewRoleRequest).EnsureSuccessStatusCode();

            //var blazorHeroContext = server.Services.GetRequiredService<BlazorHeroContext>();
            //var roleId = blazorHeroContext.Roles.SingleAsync(r => r.NormalizedName == RoleControllerValues.NewRoleRequest.Name.ToUpperInvariant()).Result.Id;

            // Act
            var result = client.Delete($"{BaseAddress}/{roleClaim.Id}");

            // Assert
            result.EnsureSuccessStatusCode();
        }

        #endregion
    }
}