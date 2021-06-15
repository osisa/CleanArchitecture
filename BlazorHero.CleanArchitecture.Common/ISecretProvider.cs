// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="ISecretProvider.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace BlazorHero.CleanArchitecture.Common
{
    public interface ISecretProvider
    {
        #region Public Properties

        public string Secret { get; }

        #endregion
    }
}