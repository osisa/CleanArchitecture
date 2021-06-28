using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.Then
{
    public class TestWithHeader
    {
        [TestMethod]
        [DataRow("trailer", "expires")]
        [DataRow("transfer-encoding", "gzip")]
        [DataRow("vary", "Version")]
        [DataRow("via", "HTTP/1.1 GWA")]
        [DataRow("upgrade", "HTTP/1.1 GWA")]
        [DataRow("warning", "110")]
        [DataRow("www-authenticate", "basic")]
        [DataRow("my-custom-header", "BOOM")]
        public async Task should_add_header(string header, string value)
        {
            var client = new MockHttpClient();

            client.When("*").Then(x=>new HttpResponseMessage()
                .WithHeader(header, value));

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://mockhttphandler.local");

            var response = await client.SendAsync(request);
            var headers = response.Headers. GetValues(header).ToList();

            headers.Count.Should().Be(1);
            headers[0].Should().Be(value);
        }

        [TestMethod]
        [DataRow("content-disposition", "inline")]
        [DataRow("content-encoding", "utf-8")]
        [DataRow("content-language", "en_US")]
        [DataRow("content-length", "0")]
        [DataRow("content-location", "http://mockhttphandler.local/location")]
        [DataRow("content-md5", "bXkgbWQ1")]
        [DataRow("content-range", "1024-2048")]
        [DataRow("content-type", "application/json")]
        [DataRow("expires", "2112-12-12")]
        [DataRow("last-modified", "2112-12-12")]
        public async Task should_add_content_headers(string header, string value)
        {
            var client = new MockHttpClient();

            client.When("*").Then(x => new HttpResponseMessage()
                .WithHeader(header, value));

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://mockhttphandler.local");

            var response = await client.SendAsync(request);
            var headers = response.Content.Headers.GetValues(header).ToList();

            headers.Count.Should().Be(1);
            headers[0].Should().Be(value);
        }

        [TestMethod]
        public async Task should_add_header_mix()
        {
            var client = new MockHttpClient();

            client.When("*").Then(x => new HttpResponseMessage()
                .WithHeader("content-disposition", "inline")
                .WithHeader("warning", "110")
                .WithHeader("content-type", "application/json")
                .WithHeader("my-custom-header", "BOOM"));

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://mockhttphandler.local");

            var response = await client.SendAsync(request);

            response.Content.Headers.GetValues("content-disposition").Single().Should().Be("inline");
            response.Content.Headers.GetValues("content-type").Single().Should().Be("application/json");
            response.Headers.GetValues("warning").Single().Should().Be("110");
            response.Headers.GetValues("my-custom-header").Single().Should().Be("BOOM");
        }


    }
}
