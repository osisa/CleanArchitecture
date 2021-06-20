﻿using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Server.Controllers.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.GetAll().Result;

            // Assert
            var okObjectResult = (OkObjectResult)result;
            var resultValue = (IResult<List<UserResponse>>)okObjectResult.Value;
            resultValue.Data.Count.Should().Be(SeededUserCount);
        }

        [TestMethod]
        public void UpdateRoles()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();

            // Act
            var result = unitUnderTest.UpdateRolesAsync(UpdateUserRolesRequest).Result;

            // Assert
            var okObjectResult = (OkObjectResult)result;
            var resultValue = (IResult<List<UserResponse>>)okObjectResult.Value;
            resultValue.Data.Count.Should().Be(SeededUserCount);
        }

        #endregion

        #region Methods

        private static UserController CreateUnitUnderTest()
        {
            var list= new List<UserResponse>(new[] { TestValues.UserResponse });
            var userService = new Mock<IUserService>();
            userService
                .Setup(i => i.GetAllAsync())
                .ReturnsAsync(Result<List<UserResponse>>.Success(list));

            return new UserController(userService.Object);
        }

        #endregion
    }
}