// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestUser.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Users
{
    public class TestUser : ITestUser
    {
        #region Constructors and Destructors

        public TestUser(string name, params string[] permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        #endregion

        #region Public Properties

        public string Name { get; }

        public IReadOnlyList<string> Permissions { get; }

        #endregion
    }
}