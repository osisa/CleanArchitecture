using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Features.ExtendedAttributes.Queries.GetAll;
using BlazorHero.CleanArchitecture.Application.Features.ExtendedAttributes.Queries.GetAllByEntityId;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Routes;
using BlazorHero.CleanArchitecture.Infrastructure.Shared;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Utilities.ExtendedAttributes.Base
{
    [TestClass]
    public class ExtendedAttributesControllerCallTests : TestBase
    {
        #region Public Methods and Operators

        


        [TestMethod]
        public void Get()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            var api = "/" + ExtendedAttributesEndpoints.GetAll("Document");

            // Act
            var result = client.Get<Result<List<GetAllExtendedAttributesResponse<string, string>>>>(api);

            // Assert
            result.Succeeded.Should().BeTrue();
        }


        [TestMethod]
        public void ByEntity()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            //var baseAddress = "/" + ExtendedAttributesEndpoints.GetAll("Document") + "/by-entity/a";

            var baseAddress = "/" + ExtendedAttributesEndpoints.GetAllByEntityId("Document","1") ;

            // Act
            var result = client.Get<Result<List<GetAllExtendedAttributesByEntityIdResponse<int,int>>>>(baseAddress);

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().Be(0);
            result.Messages.Count.Should().Be(0);
        }

        //[TestMethod]
        //public void GetChatUsersAsync()
        //{
        //    // Arrange
        //    var webHostBuilder = CreateWebHostBuilder();

        //    using var server = new TestServer(webHostBuilder);
        //    using var client = server.CreateClient();

        //    // Act
        //    var result = client.Get<Result<List<ChatUserResponse>>>($"{BaseAddress}/users");

        //    // Assert
        //    result.Succeeded.Should().BeTrue();
        //    result.Messages.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void SaveMessageAsync()
        //{
        //    // Arrange
        //    var webHostBuilder = CreateWebHostBuilder();

        //    using var server = new TestServer(webHostBuilder);
        //    using var client = server.CreateClient();

        //    // Act
        //    var result = client.Post($"{BaseAddress}", ChatsControllerValues.History);

        //    // Assert
        //    result.EnsureSuccessStatusCode();
        //}
        
        #endregion
    }
}