// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestTestValues.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using FluentAssertions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestTestInfrastructureTests
{
    [TestClass]
    public class TestTestValues
    {
        #region Public Methods and Operators

        [TestMethod]
        public void ConstructorCreateWebHost()
        {
            // Arrange
            var hostBuilder = TestValues.CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetService<IConfiguration>();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion
    }
}