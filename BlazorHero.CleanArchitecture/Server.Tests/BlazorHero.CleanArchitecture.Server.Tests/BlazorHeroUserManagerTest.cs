using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Users;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class BlazorHeroUserManagerTest
    {
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
            var unitUnderTest=CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.Users.ToArray();

            // Assert
            result.Length.Should().Be(1);
        }

        private static  UserManager<BlazorHeroUser> CreateUnitUnderTest()
        {
            return TestValues.CreateBlazorHeroUserManager();
        }
    }
}
