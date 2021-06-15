using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Common.Content;
using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.Scenario
{
    [TestClass]
    public class TestHttpRepository
    {
        public class Widget
        {
            public string Name { get; set; }

            public int Rating { get; set; }
        }

        public readonly HttpClient Repo;

        public TestHttpRepository()
        {
            // arrange
            var repo = new MockHttpClient();

            // act
            repo.When(putRequest => putRequest.HasMethod(HttpMethod.Put))
                .Then(async putRequest =>
                {
                    var requestString = await putRequest.Content.ReadAsStringAsync();
                    repo.When(HttpMethod.Get, putRequest.RequestUri)
                        .Then(x => new HttpResponseMessage(HttpStatusCode.OK)
                            .WithStringContent(
                                requestString,
                                mediaType: putRequest.Content.Headers.ContentType.MediaType));
                    return new HttpResponseMessage(HttpStatusCode.OK);
                });

            // assert
            repo.BaseAddress = new Uri("http://mockhttpclient.local");
            Repo = repo;
        }

        [TestMethod]
        public async Task should_fetch_posted_content()
        {
            // act
            await Repo.PutAsync("http://mockhttpclient.local/widget/1", new JsonContent(new Widget()
            {
                Name = "Foo",
                Rating = 10
            }));

            var widget1Response = await Repo.GetAsync("/widget/1");
            var widget1 = await widget1Response.Content.ReadAsJson<Widget>();

            // assert
            widget1Response.StatusCode.Should().Be(HttpStatusCode.OK);

             widget1.Name.Should().Be("Foo");
             widget1.Rating.Should().Be(10);
        }

        [TestMethod]
        public async Task should_fetch_latest_posted_content()
        {
            // act
            await Repo.PutAsync("http://mockhttpclient.local/widget/1", new JsonContent(new Widget()
            {
                Name = "Foo",
                Rating = 15
            }));

            await Repo.PutAsync("http://mockhttpclient.local/widget/1", new JsonContent(new Widget()
            {
                Name = "Foo-2",
                Rating = 25
            }));

            // assert
            var widget1Response = await Repo.GetAsync("/widget/1");
            var widget1 = await widget1Response.Content.ReadAsJson<Widget>();
            
            widget1Response.StatusCode.Should().Be(HttpStatusCode.OK);
            widget1.Name.Should().Be("Foo-2");
            widget1.Rating.Should().Be(25);
        }

        [TestMethod]
        public async Task should_store_multiple()
        {
            // act
            await Repo.PutAsync("http://mockhttpclient.local/widget/1", new JsonContent(new Widget()
                                                                                        {
                                                                                            Name = "Foo",
                                                                                            Rating = 15
                                                                                        }));

            await Repo.PutAsync("http://mockhttpclient.local/widget/2", new JsonContent(new Widget()
                                                                                        {
                                                                                            Name = "Bar",
                                                                                            Rating = 25
                                                                                        }));

            await Repo.PutAsync("http://mockhttpclient.local/widget/3", new JsonContent(new Widget()
                                                                                        {
                                                                                            Name = "Zut",
                                                                                            Rating = 10
                                                                                        }));

            // assert
            var widget1Response = await Repo.GetAsync("/widget/1");
            var widget1 = await widget1Response.Content.ReadAsJson<Widget>();
            widget1Response.StatusCode.Should().Be(HttpStatusCode.OK);
            widget1.Name.Should().Be("Foo");
            widget1.Rating.Should().Be(15);

            var widget2Response = await Repo.GetAsync("/widget/2");
            var widget2 = await widget2Response.Content.ReadAsJson<Widget>();
            widget2Response.StatusCode.Should().Be(HttpStatusCode.OK);
            widget2.Name.Should().Be("Bar");
            widget2.Rating.Should().Be(25);

            var widget3Response = await Repo.GetAsync("/widget/3");
            var widget3 = await widget3Response.Content.ReadAsJson<Widget>();
            widget3Response.StatusCode.Should().Be(HttpStatusCode.OK);
            widget3.Name.Should().Be("Zut");
            widget3.Rating.Should().Be(10);
        }
    }
}
