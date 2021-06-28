// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestWithXmlContent.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.Then
{
    public class Name
    {
        #region Public Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion
    }

    [TestClass]
    public class TestWithXmlContent
    {
        #region Public Methods and Operators

        [TestMethod]
        public async Task should_create_content()
        {
            var client = new MockHttpClient();

            client.When("*").Then(
                x => new HttpResponseMessage()
                    .WithXmlContent(new Name { FirstName = "John", LastName = "Doe", }));

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://mockhttphandler.local");

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("<FirstName>John</FirstName>");
            response.Content.Headers.ContentType.MediaType.Should().Be("application/xml");
        }

        [TestMethod]
        public async Task should_create_content_with_media_type()
        {
            var client = new MockHttpClient();

            client.When("*").Then(
                x => new HttpResponseMessage()
                    .WithXmlContent(new Name { FirstName = "John", LastName = "Doe", }, "application/vnd.mockhttphandler.local"));

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://mockhttphandler.local");

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("<FirstName>John</FirstName>");
            result.Should().Contain("<LastName>Doe</LastName>");

            response.Content.Headers.ContentType.MediaType.Should().Be("application/vnd.mockhttphandler.local");
        }

        #endregion
    }
}