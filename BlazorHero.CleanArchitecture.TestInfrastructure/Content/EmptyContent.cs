// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="EmptyContent.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Content
{
    /// <summary>
    ///     Represents an empty content
    /// </summary>
    /// <seealso cref="System.Net.Http.ByteArrayContent" />
    public class EmptyContent : ByteArrayContent
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmptyContent" /> class.
        /// </summary>
        public EmptyContent()
            : base(new byte[0])
        {
        }

        #endregion
    }
}