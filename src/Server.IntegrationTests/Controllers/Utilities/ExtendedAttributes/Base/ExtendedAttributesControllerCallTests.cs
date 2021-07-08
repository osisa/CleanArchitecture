using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Features.ExtendedAttributes.Queries.GetAll;
using BlazorHero.CleanArchitecture.Application.Features.ExtendedAttributes.Queries.GetAllByEntityId;
using BlazorHero.CleanArchitecture.Application.Features.ExtendedAttributes.Queries.GetById;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Routes;
using BlazorHero.CleanArchitecture.Infrastructure.Shared;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
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
        public void GetAllByEntityId()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            var baseAddress = "/" + ExtendedAttributesEndpoints.GetAllByEntityId(ExtendedAttributeControllerValues.EntityName, ExtendedAttributeControllerValues.EntityId);

            // Act
            var result = client.Get<Result<List<GetAllExtendedAttributesByEntityIdResponse<int, int>>>>(baseAddress);

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().Be(0);
            result.Messages.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            var baseAddress = "/" + ExtendedAttributesEndpoints.GetAll(ExtendedAttributeControllerValues.EntityName);

            // Act
            var result = client.Get<Result<GetExtendedAttributeByIdResponse<int, int>>>($"{baseAddress}/{ExtendedAttributeControllerValues.EntityId}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeNull();
            result.Messages.Count.Should().Be(0);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            var baseAddress = "/" + ExtendedAttributesEndpoints.GetAll(ExtendedAttributeControllerValues.EntityName);

            // Act
            var result = client.Post<Result<GetExtendedAttributeByIdResponse<int, int>>>($"{baseAddress}/{ExtendedAttributeControllerValues.EntityId}");

            // Assert
            result.Should().NotBeNull();
        }

        #endregion
    }
}