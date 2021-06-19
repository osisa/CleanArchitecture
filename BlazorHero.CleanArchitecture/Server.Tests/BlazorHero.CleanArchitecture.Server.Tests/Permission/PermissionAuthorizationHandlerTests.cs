
using System;

using BlazorHero.CleanArchitecture.Server.Permission;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests.Permission
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


        [TestMethod]
        public void HandleAsync_ForUserWithDefaultClaim_ShouldDoNothing2()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();
            var authorizationRequirement = new PermissionRequirement("x");
            
            var authorizationHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[]{ authorizationRequirement}, CreateClaimsPrincipal(), Resource);
            
            // Act
            Action result=()=>unitUnderTest.HandleAsync(authorizationHandlerContext);

            // Assert
            result.Should().NotThrow();

        }

        [TestMethod]
        public void HandleAsync_ForEmptyUser_ShouldDoNothing()
        {
            // Arrange
            var unitUnderTest = CreateUnitUnderTest();
            var authorizationRequirement = new PermissionRequirement("x");

            var authorizationHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[] { authorizationRequirement }, null, Resource);

            // Act
            Action result = () => unitUnderTest.HandleAsync(authorizationHandlerContext);

            // Assert
            result.Should().NotThrow();

        }

        //[TestMethod]
        //public void HandleAsync2()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateUnitUnderTest();

        //    var authorizationHandlerContext = new PermissionAuthorizationHandler(); //new IAuthorizationRequirement[] { }, CreateClaimsPrincipal(), Resource);

        //    // Act
        //    Action result = () => unitUnderTest.;

        //    // Assert
        //    result.Should().NotThrow();

        //}
        #endregion

        #region Methods

        private static IAuthorizationHandler CreateUnitUnderTest() => new PermissionAuthorizationHandler();
        
        #endregion
    }
}