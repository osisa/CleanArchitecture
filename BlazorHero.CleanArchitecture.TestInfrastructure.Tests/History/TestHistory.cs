// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestHistory.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Tests.History
{
    [TestClass]
    public class TestHistory
    {
        #region Public Methods and Operators

        [TestMethod]
        public async Task Should_record_request_history()
        {
            var client = new MockHttpClient();

            client.When("*").Then(HttpStatusCode.OK);

            await client.GetAsync("http://mockhttphandler.local/1");
            await client.GetAsync("http://mockhttphandler.local/2");
            await client.GetAsync("http://mockhttphandler.local/3");

            client.RequestHistory.Count.Should().Be(3);
            client.RequestHistory[0].RequestUri.AbsolutePath.Should().Be("/1");
            client.RequestHistory[1].RequestUri.AbsolutePath.Should().Be("/2");
            client.RequestHistory[2].RequestUri.AbsolutePath.Should().Be("/3");
        }

        #endregion
    }
}