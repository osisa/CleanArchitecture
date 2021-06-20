using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Users;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Services.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;

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
        
        private static IUserService CreateUnitUnderTest()
        {
            //var hostBuilder = CreateWebHostBuilder();
            //var services = hostBuilder.Build().Services;
            
            //return services.GetRequiredService<IUserService>();
            var userManager = new Mock<UserManager<BlazorHeroUser>>();
            var mapper = new Mock<IMapper>();
            var roleManager = new Mock<RoleManager<BlazorHeroRole>>();
            var mailService = new Mock<IMailService>();
            var localizer = new Mock<IStringLocalizer<UserService>>();
            var excelService = new Mock<IExcelService>();
            var currentUserService = new Mock<ICurrentUserService>();

            return new UserService(userManager.Object, mapper.Object, roleManager.Object, mailService.Object, localizer.Object, excelService.Object, currentUserService.Object);
        }
        #endregion
    }
}