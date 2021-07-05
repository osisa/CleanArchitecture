using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Shared;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Communication
{
    [TestClass]
    public class ChatControllerCallTests : TestBase
    {
        #region Constants

        private const string BaseAddress = "/api/chats";

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void GetChatHistoryAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();
            
            // Act
            var result = client.Get<Result<List<ChatHistoryResponse>>>($"{BaseAddress}/{ChatsControllerValues.ContactId}");

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        [TestMethod]
        public void GetChatUsersAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<List<ChatUserResponse>>>($"{BaseAddress}/users");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Messages.Count.Should().Be(0);
        }

        [TestMethod]
        public void SaveMessageAsync()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}", ChatsControllerValues.History);

            // Assert
            result.EnsureSuccessStatusCode();
        }
        
        #endregion
    }
}