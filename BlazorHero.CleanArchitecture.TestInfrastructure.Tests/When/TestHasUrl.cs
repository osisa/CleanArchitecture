// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestHasUrl.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Threading.Tasks;

using BlazorHero.CleanArchitecture.TestInfrastructure.Exceptions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.When
{
    [TestClass]
    public class TestHasUrl
    {
        #region Public Methods and Operators

        [TestMethod]
        [DataRow("not-a-relative-url")]
        public void When_invalid_urlQuery_should_throw(string urlQuery)
        {
            // arrange
            var client = new MockHttpClient();

            // act
            Action result = () => client.When(urlQuery).Then(HttpStatusCode.OK);

            // assert
            result.Should().Throw<InvalidUrlQueryException>();
        }

        [TestMethod]
        [DataRow("http://mockhttpclient.local/path/to/resource?q1=a&q2=b", "?q1=a", "?q2=b", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q1=a&q2=b", "?q1=a", "?q2=c", false)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q1=a&q2=b", "//mockhttpclient.local", "/path/*", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q1=a&q2=b", "https://mockhttpclient.local", "/path/*", false)]
        public void When_multiple_conditions_should_match_all(string requestUri, string queryUri1, string queryUri2, bool match)
        {
            // arrange
            var expectedStatusCode = match ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var client = new MockHttpClient();

            client.When(queryUri1)
                .When(queryUri2)
                .Then(HttpStatusCode.OK);

            // act
            var result = client.GetAsync(requestUri).Result;

            // assert
            result.StatusCode.Should().Be(expectedStatusCode);
        }

        [TestMethod]
        [DataRow("http://mockhttpclient.local/path/to/resource", "http://mockhttpclient.local/path/to/resource", true)]
        [DataRow("http://mockHTTPclient.local/path/to/resource", "http://MOCKhttpCLIENT.local/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q=a", "/path/to/resource?q=a", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q1=a&q2=b", "/path/to/resource?q1=a&q2=b", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q1=a&q2=b", "/path/to/resource?q2=b&q1=a", false)]
        public void When_url_condition_should_match(string requestUri, string queryUri, bool match)
        {
            // arrange
            var expectedStatusCode = match ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var client = new MockHttpClient();

            client.When(new Uri(queryUri, UriKind.RelativeOrAbsolute))
                .Then(HttpStatusCode.OK);

            // act
            var result = client.GetAsync(requestUri).Result;

            // assert
            result.StatusCode.Should().Be(expectedStatusCode);
        }

        [TestMethod]
        [DataRow("http://mockhttpclient.local/path/to/resource", "", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "*", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "http://mockhttpclient.local/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "http*://mockhttpclient.local/path/to/resource", true)]
        [DataRow("https://mockhttpclient.local/path/to/resource", "http*://mockhttpclient.local/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "http://*.local/path/to/resource", true)]
        [DataRow("http://mockHTTPclient.local/path/to/resource", "http://MOCKhttpCLIENT.local/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "//mockhttpclient.local/path/to/resource", true)]
        [DataRow("https://mockhttpclient.local/path/to/resource", "//mockhttpclient.local/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "/path/TO/resource/", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "/path/*/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "/path/*", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource", "/path/to/resource/", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource/", "/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource/", "/path/to/resource/", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q=test", "/path/to/resource", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q=test", "/path/to/resource?q=test", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q=test", "/path/to/resource?q=*", true)]
        [DataRow("http://mockhttpclient.local/path/to/resource?q=test", "/path/to/resource?q=false", false)]
        [DataRow("http://mockhttpclient.local?q1=a&q2=b", "?q1=a", true)]
        [DataRow("http://mockhttpclient.local/?q1=a&q2=b", "?q1=a", true)]
        [DataRow("http://mockhttpclient.local?q1=a&q2=b", "?q2=b", true)]
        [DataRow("http://mockhttpclient.local?q1=a&q2=b", "?q1=a&q2=b", true)]
        [DataRow("http://mockhttpclient.local?q1=a&q2=b", "?q2=b&q1=a", true)]
        [DataRow("http://mockhttpclient.local?q1=a&q2=b", "?q1=a&q2=a", false)]
        [DataRow("http://mockhttpclient.local?q1=a&q2=b", "?q1=a&q2=b&q3=c", false)]
        [DataRow("http://mockhttpclient.local?q=a&q=b", "?q=a&q=b", true)]
        [DataRow("http://mockhttpclient.local?q=a&q=b", "?q=a&q=a", false)]
        [DataRow("http://mockhttpclient.local?q=a&q=a", "?q=a&q=a", true)]
        [DataRow("http://mockhttpclient.local?q=a&q=a", "?q=a&q=a&q=a", false)]
        [DataRow("http://mockhttpclient.local?q=a&q=a", "?q=a&q=*", true)]
        [DataRow("http://mockhttpclient.local?q1=a&q1=b&q2=a&q2=b", "?q1=a&q1=b&q2=b", true)]
        public void  When_url_query_condition_should_match(string requestUri, string pathRule, bool match)
        {
            // arrange
            var expectedStatusCode = match ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var client = new MockHttpClient();

            client
                .When(pathRule)
                .Then(HttpStatusCode.OK);

            // act
            var result = client.GetAsync(requestUri).Result;

            // assert
            result.StatusCode.Should().Be(expectedStatusCode);
        }

        #endregion
    }
}