// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="CounterTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using BlazorHero.CleanArchitecture.Client.Pages.Identity;

using Bunit;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Client.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Client.Tests
{
    [TestClass]
    public class CounterTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void Rendering()
        {
            // Arrange
            using var ctx = GetBlazorTestContext();
            
           // Act
            var cut = ctx.RenderComponent<Account>();

            // Assert
            cut.FindAll("div").Count.Should().Be(45);
        }

        #endregion
    }
}