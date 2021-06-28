// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="ITestUser.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Users
{
    public interface ITestUser
    {
        #region Public Properties

        public string Name { get; }

        public IReadOnlyList<string> Permissions { get; }

        #endregion
    }
}