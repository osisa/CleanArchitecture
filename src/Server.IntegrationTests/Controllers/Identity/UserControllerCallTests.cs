using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Shared;
using BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Identity
{
    [TestClass]
    public class UserControllerCallTests : TestBase
    {
        #region Constants

        private const string BaseAddress = "/api/identity/user";

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void CheckOrigin()
        {
            // Arrange
            
            // Act
            var result = new Uri(string.Concat($"{Origin}/", Route));

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void ConfirmEmailAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var userManager = server.Services.GetRequiredService<UserManager<BlazorHeroUser>>();
            var userStore = server.Services.GetRequiredService<IUserStore<BlazorHeroUser>>();
            var user = userStore.FindByIdAsync(Id0, CancellationToken.None).Result; 
            var code = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var code64 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Act
            var result = client.GetAsync($"{BaseAddress}/confirm-email?userId={Id0}&code={code64}").Result;

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void Export()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();
         
            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();
        
            // Act
            var result = client.GetStringAsync($"{BaseAddress}/export").Result;

            // Assert
            result.Should().NotBeEmpty();

            //todo: translate export data

            //result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void ForgotPasswordAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            // Act
            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();
            
            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseAddress}/forgot-password");
            var data = JsonConvert.SerializeObject(ForgotPasswordRequest);

            StringContent httpContent = new(data, Encoding.UTF8, HttpClientExtensions.ApplicationJson);
            request.Content = httpContent;
            request.Headers.Add("origin", Origin);

            // Act
            //var result = client.PostAsync("/api/identity/user/forgot-password", ForgotPasswordRequest);
            var result = client.SendAsync(request).Result;

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<List<UserResponse>>>($"{BaseAddress}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Count.Should().Be(2);
            result.Data[0].Should().BeEquivalentTo(TestValues.UserResponse);

            server.Host.StopAsync().GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetAuthenticationHandler()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();
            var host = webHostBuilder.Build();

            // Act
            var result = host.Services.GetService<IAuthenticationHandler>();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            // Act
            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<UserResponse>>($"{BaseAddress}/{Id0}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(TestValues.UserResponse);
        }

        [TestMethod]
        public void GetRolesForUserIdAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();
            
            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();
            
            // Act
            var result = client.Get<Result<UserRolesResponse>>($"{BaseAddress}/roles/{Id0}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.UserRoles.Should().BeEquivalentTo(TestValues.UserRolesResponse.UserRoles);
        }

        [TestMethod]
        public void RegisterAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();
            
            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}", RegisterRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void ResetPasswordAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}/reset-password", ResetPasswordRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void ToggleUserStatusAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}/toggle-status", ToggleUserStatusRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void UpdateRolesAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Put($"{BaseAddress}/roles/{UpdateUserRolesRequest.UserId}", UpdateUserRolesRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        #endregion
    }
}