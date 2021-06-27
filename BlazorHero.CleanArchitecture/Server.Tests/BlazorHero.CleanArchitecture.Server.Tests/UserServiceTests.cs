using System.Collections.Generic;

using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Services.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserServiceTests
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
        public void GetAll()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

           // Act
            var result = unitUnderTest.GetAllAsync().Result;

            // Assert
            result.Data.Count.Should().Be(1);
            result.Data[0].Id.Should().Be(TestValues.Id);
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

        [TestMethod]
        public void Get()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

           // Act
            var result = unitUnderTest.GetAsync(Id).Result;

            // Assert
            result.Data.Id.Should().Be(Id);
        }
        
        private static UserService CreateUnitUnderTest()
        {
            var userManager = new MockUserManager();//TestValueFactory.CreateBlazorHeroUserManager();//new Mock<UserManager<BlazorHeroUser>>();

            //var mapper = new Mock<IMapper>();
            //mapper.Setup(i => i.Map<List<UserResponse>>(It.IsAny<List<BlazorHeroUser>>())).Returns(new List<UserResponse> { TestValues.UserResponse });  // _mapper.Map<List<UserResponse>>(users);
            //mapper.Setup(i => i.Map<UserResponse>(It.IsAny<BlazorHeroUser>())).Returns(TestValues.UserResponse );  // _mapper.Map<List<UserResponse>>(users);

            //var mapp = new AutoMapper.AdvancedConfiguration().;
            var mapper = TestValueFactory.CreateMapper();

            var roleManager = TestValueFactory.CreateRoleManager();
            var mailService = new Mock<IMailService>().Object;
            var localizer = new Mock<IStringLocalizer<UserService>>().Object;
            var excelService = new Mock<IExcelService>().Object;
            var currentUserService = new Mock<ICurrentUserService>().Object;

            //return ActivatorUtilities.CreateInstance<UserService>()

            return new UserService(userManager, mapper, roleManager, mailService, localizer, excelService, currentUserService);
        }
        #endregion
    }
}