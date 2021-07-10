using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Infrastructure.Shared;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.v1.Catalog
{
    namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.v1
    {
        [TestClass]
        public class ProductsControllerCallTests : TestBase
        {
            #region Constants

            //private const string BaseAddress = $"api/v{version:apiVersion}/dashboard";
            private const string BaseAddress = "api/v1/products";

            #endregion

            #region Public Methods and Operators

            [TestMethod]
            public void GetAll()
            {
                // Arrange  
                var webHostBuilder = CreateWebHostBuilder();

                using var server = new TestServer(webHostBuilder);
                using var client = server.CreateClient();

                // Act
                var result = client.Get<PaginatedResult<GetAllPagedProductsResponse>>($"{BaseAddress}");

                // Assert
                result.Succeeded.Should().BeTrue();
                result.TotalCount.Should().Be(0);
                //result.Data.UserCount.Should().BeGreaterOrEqualTo(2);
                //result.EnsureSuccessStatusCode();
            }


            [TestMethod]
            public void Post()
            {
                // Arrange  
                var webHostBuilder = CreateWebHostBuilder();

                using var server = new TestServer(webHostBuilder);
                using var client = server.CreateClient();

                // Act
                //var result = client.Post($"{BaseAddress}", );

                // Assert
                //result.Succeeded.Should().BeTrue();
                //result.TotalCount.Should().Be(0);
                //result.Data.UserCount.Should().BeGreaterOrEqualTo(2);
                //result.EnsureSuccessStatusCode();
            }

            #endregion
        }
    }
}