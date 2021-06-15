// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestHasHeader.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.When
{
    [TestClass]
    public class TestHasHeader
    {
        #region Public Methods and Operators

        [TestMethod]
        public void When_content_header_condition_should_match()
        {
            // arrange
            var client = new MockHttpClient();

            client.When(x => x.HasHeader("content-type", "application/json"))
                .Then(HttpStatusCode.OK);

            // act
            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local"), 
                              Content = new StringContent("")
                          };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // assert
            client.SendAsync(request).Result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void When_header_condition_should_match()
        {
            // arrange
            var client = new MockHttpClient();

            client
                .When(x => x.HasHeader("version", "1"))
                .Then(HttpStatusCode.OK);

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local")
                          };

            request.Headers.Add("version", "1");
            
            // act
            var result= client.SendAsync(request).Result;

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void When_header_without_value_should_match_any_value()
        {
            // arrange
            var client = new MockHttpClient();

            client
                .When(x => x.HasHeader("version"))
                .Then(HttpStatusCode.OK);

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local")
                          };

            request.Headers.Add("version", "4");

            // act
            var result= client.SendAsync(request).Result;

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void When_multiple_headers_condition_should_match_any()
        {
            // arrange
            var client = new MockHttpClient();

            client
                .When(x => x.HasHeader("Accept", "application/json"))
                .Then(HttpStatusCode.OK);

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local")
                          };

            request.Headers.Add("Accept", "application/xml");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept", "text/html");


            // act
            var result= client.SendAsync(request).Result.StatusCode;

            // assert
            result.Should().Be(HttpStatusCode.OK);
        }

        #endregion
    }
}