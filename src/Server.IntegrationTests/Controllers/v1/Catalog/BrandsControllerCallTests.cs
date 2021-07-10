using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Features.Brands.Queries.GetAll;
using BlazorHero.CleanArchitecture.Infrastructure.Shared;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.v1.Catalog
{
    [TestClass]
    public class BrandsControllerCallTests
    {
        #region Constants

        private const string BaseAddress = "api/v1/brands";

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void Delete()
        {
            // Arrange  
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            //ensure Brand
            var resultEnsure = client.Post($"{BaseAddress}", BrandControllerValues.CreateAddEditBrandCommand());
            resultEnsure.EnsureSuccessStatusCode();

            var getAll = client.Get<Result<List<GetAllBrandsResponse>>>($"{BaseAddress}");
            getAll.Data.Count.Should().BeGreaterOrEqualTo(1);
            var brand0 = getAll.Data.ToArray()[0];

            // Act
            var result = client.Delete($"{BaseAddress}/{brand0.Id}");

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange  
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            //ensure Brand
            var resultEnsure = client.Post($"{BaseAddress}", BrandControllerValues.CreateAddEditBrandCommand());
            resultEnsure.EnsureSuccessStatusCode();

            var getAll = client.Get<Result<List<GetAllBrandsResponse>>>($"{BaseAddress}");
            getAll.Data.Count.Should().BeGreaterOrEqualTo(1);
            var brand0 = getAll.Data.ToArray()[0];

            // Act
            var result = client.Get<Result<GetAllBrandsResponse>>($"{BaseAddress}/{brand0.Id}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Id.Should().Be(brand0.Id);
        }

        [TestMethod]
        public void Export()
        {
            // Arrange  
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

           
            // Act
            var result = client.Get<Result<string>>($"{BaseAddress}/export");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange  
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Get<Result<List<GetAllBrandsResponse>>>($"{BaseAddress}");

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        [TestMethod]
        public void Post()
        {
            // Arrange  
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}", BrandControllerValues.CreateAddEditBrandCommand());

            // Assert
            result.EnsureSuccessStatusCode();
        }

        #endregion
    }
}