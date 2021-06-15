// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="InvalidUrlQueryString.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Exceptions
{
    /// <summary>
    ///     Represents error that occurs when creating a <see cref="UrlQuery" /> with an invalid string.
    /// </summary>
    /// <seealso cref="Exception" />
    public class InvalidUrlQueryException : Exception
    {
        #region Fields

        /// <summary>
        ///     The invalid query string.
        /// </summary>
        public readonly string UrlQuery;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidUrlQueryException" /> class.
        /// </summary>
        /// <param name="urlQuery">The invalid query string.</param>
        public InvalidUrlQueryException(string urlQuery)
            : base(
                $@"UrlQuery of '{urlQuery}' is not in the correct format. 
Try starting the query with either 'http://' for absoulte url, '//' for schema relative url, '/' for relative urls or '?' for query string only queries")
        {
            UrlQuery = urlQuery;
        }

        #endregion
    }
}