
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
    public class AccountControllerCallTests : TestBase
    {
        #region Constants

        private const string BaseAddress = "/api/identity/account";

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void ChangePassword()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Put($"{BaseAddress}/{nameof(ChangePassword)}", AccountControllerValues.ChangePasswordRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void GetProfilePictureAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result>($"{BaseAddress}/profile-picture/{UserResponse.Id}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Messages.Count.Should().Be(0);
        }

        [TestMethod]
        public void UpdateProfile()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Put($"{BaseAddress}/{nameof(UpdateProfile)}", AccountControllerValues.UpdateProfileRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void UpdateProfilePictureAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}/profile-picture/{UserResponse.Id}", AccountControllerValues.UpdateProfilePictureRequest);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        #endregion
    }
}