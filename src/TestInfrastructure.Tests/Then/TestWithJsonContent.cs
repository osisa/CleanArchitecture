// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestWithJsonContent.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.Then
{
    [TestClass]
    public class TestWithJsonContent
    {
        #region Public Methods and Operators

        [TestMethod]
        public async Task should_create_content()
        {
            // arrange
            var client = new MockHttpClient();
            client.When("*").Then(
                x => new HttpResponseMessage()
                    .WithJsonContent(new { FirstName = "John", LastName = "Doe", }));

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local")
                          };

            // act
            var response = client.SendAsync(request).Result;
            
            // assert
            var responseObj = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync())!;
            ((string)responseObj.FirstName).Should().Be("John");
            ((string)responseObj.LastName).Should().Be("Doe");

            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
        }

        [TestMethod]
        public async Task should_create_content_with_media_type()
        {
            // arrange
            var client = new MockHttpClient();

            client.When("*").Then(
                x => new HttpResponseMessage()
                    .WithJsonContent(new
                                     {
                                         FirstName = "John", 
                                         LastName = "Doe",
                                     }, 
                        "application/vnd.mockhttphandler.local"));

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local")
                          };

            // act
            var response = client.SendAsync(request).Result;

            // assert
            var responseObj = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            ((string)responseObj.FirstName).Should().Be("John");
            ((string)responseObj.LastName).Should().Be("Doe");

            response.Content.Headers.ContentType.MediaType.Should().Be("application/vnd.mockhttphandler.local");
        }

        [TestMethod]
        public async Task should_create_content_with_media_type_and_serializer_settings()
        {
            // arrange
            var client = new MockHttpClient();

            client.When("*").Then(
                x => new HttpResponseMessage()
                    .WithJsonContent(new
                                     {
                                         FirstName = "John", 
                                         LastName = "Doe",
                                     }, 
                        "application/vnd.mockhttphandler.local", 
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri("http://mockhttphandler.local")
                          };

            // act
            var response = await client.SendAsync(request);

            // assert
            var responseObj = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            ((string)responseObj.firstName).Should().Be("John");
            ((string)responseObj.lastName).Should().Be("Doe");

            response.Content.Headers.ContentType.MediaType.Should().Be("application/vnd.mockhttphandler.local");
        }

        #endregion
    }
}