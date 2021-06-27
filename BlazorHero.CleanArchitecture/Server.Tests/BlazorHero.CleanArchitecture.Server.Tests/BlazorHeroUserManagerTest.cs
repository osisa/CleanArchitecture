using System.Linq;

using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class BlazorHeroUserManagerTest
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

        [TestMethod]
        public void Users()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.Users.ToArray();

            // Assert
            result.Length.Should().Be(1);
        }

        #endregion

        #region Methods

        private static UserManager<BlazorHeroUser> CreateUnitUnderTest()
        {
            return TestValueFactory.CreateBlazorHeroUserManager();
        }

        #endregion
    }
}