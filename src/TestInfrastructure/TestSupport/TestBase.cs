using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport
{
    public abstract class TestBase
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the test context.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public TestContext TestContext { get; set; }

        #endregion
    }
}