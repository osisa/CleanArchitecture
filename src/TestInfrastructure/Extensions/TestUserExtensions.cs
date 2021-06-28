// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="Class1.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using BlazorHero.CleanArchitecture.TestInfrastructure.Users;

using Microsoft.Extensions.DependencyInjection;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Extensions
{
    public static class TestUserExtensions
    {
        #region Public Methods and Operators

        public static void AddTestUser(this IServiceCollection @this, ITestUser testUser)
        {
            @this.AddSingleton(testUser);
        }

        #endregion
    }
}