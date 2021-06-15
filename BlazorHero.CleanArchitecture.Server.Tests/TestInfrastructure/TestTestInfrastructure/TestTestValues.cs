// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestTestValues.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using FluentAssertions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestTestInfrastructure
{
    [TestClass]
    public class TestTestValues
    {
        #region Public Methods and Operators

        [TestMethod]
        public void ConstructorCreateWebHost()
        {
            // arrange
            var hostBuilder = TestValues.CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

            // act
            var result = services.GetService<IConfiguration>();

            // assert
            result.Should().NotBeNull();
        }

        #endregion
    }
}