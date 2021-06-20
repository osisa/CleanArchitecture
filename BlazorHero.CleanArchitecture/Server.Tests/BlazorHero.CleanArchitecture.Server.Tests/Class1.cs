using System;
using System.Collections.Generic;

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