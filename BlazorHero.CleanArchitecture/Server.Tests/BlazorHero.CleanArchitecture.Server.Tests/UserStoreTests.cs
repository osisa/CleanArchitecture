using System.Threading;

using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Services.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserStoreTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void CreateAsync()
        {
            // Arrange
            var unitUnderTest = CreateUserStore();
            
           // Act
            var result = unitUnderTest.CreateAsync(User, CancellationToken.None).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        //[TestMethod]
        //public void FindByNameAsync()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateUserStore();

        //    // Act
        //    var result = unitUnderTest.FindByNameAsync(TestValues.Test)

        //    // Assert
        //    result.Succeeded.Should().BeTrue();
        //}

        //[TestMethod]
        //public void UpdateRolesAsync()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateUnitUnderTest();

        //    // Act
        //    var result = unitUnderTest.UpdateRolesAsync(UpdateUserRolesRequest).Result;

        //    // Assert
        //    result.Succeeded.Should().BeTrue();
        //    result.Messages.Count.Should().Be(1);
        //}

        //[TestMethod]
        //public void Get()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateUnitUnderTest();

        //   // Act
        //    var result = unitUnderTest.GetAsync(Id0).Result;

        //    // Assert
        //    result.Data.Id.Should().Be(Id0);
        //}

        //private static IUserService CreateUnitUnderTest()
        //{
        //    var userManager = CreateUserManager();//new Mock<UserManager<BlazorHeroUser>>();
        //    var mapper = new Mock<IMapper>();
        //    var roleManager = new Mock<RoleManager<BlazorHeroRole>>();
        //    var mailService = new Mock<IMailService>();
        //    var localizer = new Mock<IStringLocalizer<UserService>>();
        //    var excelService = new Mock<IExcelService>();
        //    var currentUserService = new Mock<ICurrentUserService>();

        //    return new UserService(userManager, mapper.Object, roleManager.Object, mailService.Object, localizer.Object, excelService.Object, currentUserService.Object);
        //}
        #endregion
    }
}