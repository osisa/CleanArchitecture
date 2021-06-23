using System;
using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Requests.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Users;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Routes;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

using Common.Web.Contracts;
using Common.Web.Contracts.JsonExtensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RichardSzalay.MockHttp;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserManagerTests
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
        public void GetAllAsync()
        {
            // Arrange
            var json = Result<List<UserResponse>>.Success(new List<UserResponse>(new[] { TestValues.UserResponse })).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(TestValues.Root + UserEndpoints.GetAll)
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.GetAllAsync().Result;

            // Assert
            result.Data.Count.Should().Be(1);
        }

        [TestMethod]
        public void GetAsync()
        {
            // Arrange
            var json = Result<UserResponse>.Success( TestValues.UserResponse ).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.Get(Id))
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.GetAsync(Id).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        
        [TestMethod]
        public void RegisterUserAsync()
        {
            // Arrange
            var json = Result<RegisterRequest>.Success(TestValues.RegisterRequest).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.Register)
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.RegisterUserAsync(TestValues.RegisterRequest).Result;
            
            // Assert
            result.Succeeded.Should().BeTrue();
        }


        [TestMethod]
        public void ToggleUserStatusAsync()
        {
            // Arrange
            var json = Result<ToggleUserStatusRequest>.Success(TestValues.ToggleUserStatusRequest).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.ToggleUserStatus)
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.ToggleUserStatusAsync(TestValues.ToggleUserStatusRequest).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }


        [TestMethod]
        public void GetRolesAsync()
        {
            // Arrange
            var json = Result<UserRolesResponse>.Success(TestValues.UserRolesResponse).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.GetUserRoles(Id))
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.GetRolesAsync(Id).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        [TestMethod]
        public void UpdateRolesAsync()
        {
            // Arrange
            var json = Result<UpdateUserRolesRequest>.Success(TestValues.UpdateUserRolesRequest).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.GetUserRoles(TestValues.UpdateUserRolesRequest.UserId))
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.UpdateRolesAsync(TestValues.UpdateUserRolesRequest).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        [TestMethod]
        public void ForgotPasswordAsync()
        {
            // Arrange
            var json = Result<ForgotPasswordRequest>.Success(TestValues.ForgotPasswordRequest).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.ForgotPassword)
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.ForgotPasswordAsync(TestValues.ForgotPasswordRequest).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        
        [TestMethod]
        public void ResetPasswordAsync()
        {
            // Arrange
            var json = Result<ResetPasswordRequest>.Success(TestValues.ResetPasswordRequest).ToJson();
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.ResetPassword)
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.ResetPasswordAsync(TestValues.ResetPasswordRequest).Result;

            // Assert
            result.Succeeded.Should().BeTrue();
        }


        [TestMethod]
        public void ExportToExcelAsync()
        {
            // Arrange
            var json =SearchString;
            var unitUnderTest = CreateUnitUnderTest(
                h => h
                    .When(Root + UserEndpoints.ExportFiltered(SearchString))
                    .Respond(Constants.ApplicationJson, json));

            // Act
            var result = unitUnderTest.ExportToExcelAsync(SearchString).Result;

            // Assert
            result.Should().Be(SearchString);
        }

        #endregion

        #region Methods

        private static UserManager CreateUnitUnderTest(Action<MockHttpMessageHandler> setup = null)
        {
            var mockHttpHandler = new MockHttpMessageHandler();
            setup?.Invoke(mockHttpHandler);

            var httpClient = mockHttpHandler.ToHttpClient();
            httpClient.BaseAddress =new Uri( Root);

            return new UserManager(httpClient);
        }

        #endregion
    }
}