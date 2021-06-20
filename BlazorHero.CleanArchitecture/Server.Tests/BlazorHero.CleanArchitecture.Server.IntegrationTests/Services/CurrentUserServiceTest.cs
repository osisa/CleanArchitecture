using BlazorHero.CleanArchitecture.Server.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Services
{
    [TestClass]
    public class CurrentUserServiceTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void ClaimsCount_ForCurrentUserService_ShouldBe1()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.Claims;

            // Assert
            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void UserId_ForCurrentUserService_ShouldBeId4()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.UserId;

            // Assert
            result.Should().Be(Id);
        }

        #endregion

        #region Methods

        private static CurrentUserService CreateUnitUnderTest()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(httpContextUser => httpContextUser.HttpContext.User).Returns(CreateClaimsPrincipal());

            return new CurrentUserService(httpContextAccessor.Object);
        }

        #endregion
    }
}