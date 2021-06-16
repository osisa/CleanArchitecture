using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetAll()
        {
            // arrange
            var unitUnderTest = CreateUnitUnderTest();

            // act
            var result = unitUnderTest.GetAllAsync().Result;

            // assert
            result.Data.Count.Should().Be(SeededUserCount);
        }

        [TestMethod]
        public void Get()
        {
            // arrange
            var unitUnderTest = CreateUnitUnderTest();

            // act
            var result = unitUnderTest.GetAsync(Id).Result;

            // assert
            result.Data.Id.Should().Be(Id);
        }
        
        private static IUserService CreateUnitUnderTest()
        {
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            return services.GetRequiredService<IUserService>();
        }
        #endregion
    }
}