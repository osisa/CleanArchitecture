using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests
{
    [TestClass]
    public class UserServiceTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void Get()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.GetAsync(Id0).Result;

            // Assert
            result.Data.Id.Should().Be(Id0);
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.GetAllAsync().Result;

            // Assert
            result.Data.Count.Should().Be(SeededUserCount);
            result.Data[0].Id.Should().Be(Id0);
            result.Data[1].Id.Should().Be(Id1);
        }

        [TestMethod]
        public void UpdateRolesAsync()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.UpdateRolesAsync(UpdateUserRolesRequest).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Messages.Count.Should().Be(1);
            //result.Data[0].Id.Should().Be(Id0);
            //result.Data[1].Id.Should().Be(Id1);
        }

        #endregion

        #region Methods

        private static IUserService CreateUnitUnderTest()
        {
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

            return services.GetRequiredService<IUserService>();
        }

        #endregion
    }
}