
using BlazorHero.CleanArchitecture.Server.Permission;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class PermissionAuthorizationHandlerTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void Constructor()
        {
            // Arrange

            // Act
            var result = CreateUnitUnderTest();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region Methods

        private static PermissionAuthorizationHandler CreateUnitUnderTest()
        {
            return new PermissionAuthorizationHandler();
        }

        #endregion
    }
}