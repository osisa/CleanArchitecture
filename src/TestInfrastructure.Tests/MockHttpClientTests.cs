// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="MockHttpClientTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests
{
    [TestClass]
    public class MockHttpClientTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void Constructor()
        {
            // arrange

            // act
            var result = CreateUnitUnderTest();

            // assert
            result.Should().NotBeNull();
        }
        
        [TestMethod]
        public void TrivialCall()
        {
            // arrange
            var uri = new Uri("http://mockhttpclient.local");
            var unitUnderTest = CreateUnitUnderTest();
            
            // act
            var result = unitUnderTest.GetAsync(uri).Result;

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        private static MockHttpClient CreateUnitUnderTest()
        {
            var unitUnderTest = new MockHttpClient();
            unitUnderTest.When(getRequest => getRequest.HasMethod(HttpMethod.Get))
                .Then(getRequest => new HttpResponseMessage(HttpStatusCode.OK));

            
            unitUnderTest.BaseAddress = new Uri("http://mockhttpclient.local");
            
            return unitUnderTest;
        }

        #endregion
    }
}