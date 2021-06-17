using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Server.Controllers.Identity;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

           // Act
            var result =  unitUnderTest.GetAll().Result;

            // Assert
            var okObjectResult = (OkObjectResult)result;
            var resultValue = (IResult<List<UserResponse>>)okObjectResult.Value;
            resultValue.Data.Count.Should().Be(SeededUserCount);
        }

        private static UserController CreateUnitUnderTest()
        {
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            var userService= services.GetRequiredService<IUserService>();

            return new UserController(userService);
        }
    }
}