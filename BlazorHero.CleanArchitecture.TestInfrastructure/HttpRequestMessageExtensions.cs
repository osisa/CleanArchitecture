// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="HttpRequestMessageExtensions.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using BlazorHero.CleanArchitecture.TestInfrastructure.Exceptions;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    /// <summary>
    ///     Provides extension methods on <see cref="HttpRequestMessage" />
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Determines whether the request contains the specified header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="headerName">Name of the header.</param>
        /// <param name="headerValue">The header value.</param>
        /// <returns>
        ///     <c>true</c> if the request contains the specified header; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     request
        ///     or
        ///     headerName
        /// </exception>
        public static bool HasHeader(this HttpRequestMessage request, string headerName, string? headerValue = null)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (headerName == null) throw new ArgumentNullException(nameof(headerName));

            IEnumerable<string> values;

            //check normal header
            if (request.Headers.TryGetValues(headerName, out values)
                && (headerValue == null || values.Any(x => x == headerValue)))
                return true;

            //check content header
            if (request.Content != null && request.Content.Headers.TryGetValues(headerName, out values)
                                        && (headerValue == null || values.Any(x => x == headerValue)))
                return true;

            return false;
        }

        /// <summary>
        ///     Determines whether the request uses the specified method.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="method">The method.</param>
        /// <returns>
        ///     <c>true</c> if the request uses the specified method; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     request
        ///     or
        ///     method
        /// </exception>
        public static bool HasMethod(this HttpRequestMessage request, HttpMethod method)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return request.Method == method;
        }

        /// <summary>
        ///     Determines whether the request matches the url.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="url">The url query in the format accpeted by <see cref="UrlQuery" />.</param>
        /// <returns>
        ///     <c>true</c> if the requset uses the specified url; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     request
        ///     or
        ///     url
        /// </exception>
        /// <exception cref="InvalidUrlQueryException">The url is not a valid format for <see cref="UrlQuery" />.</exception>
        public static bool HasUrl(this HttpRequestMessage request, string url)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (url == null) throw new ArgumentNullException(nameof(url));

            return HasUrl(request, new UrlQuery(url));
        }

        /// <summary>
        ///     Determines whether the request matches the url.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="urlQuery">The url query.</param>
        /// <returns>
        ///     <c>true</c> if the requset uses the specified url; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     request
        ///     or
        ///     urlQuery
        /// </exception>
        public static bool HasUrl(this HttpRequestMessage request, UrlQuery urlQuery)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (urlQuery == null) throw new ArgumentNullException(nameof(urlQuery));

            return urlQuery.Match(request.RequestUri);
        }

        /// <summary>
        ///     Determines whether the request matches the url.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="url">The url to compare with an exact match.</param>
        /// <returns>
        ///     <c>true</c> if the requset uses the specified url; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     request
        ///     or
        ///     url
        /// </exception>
        public static bool HasUrl(this HttpRequestMessage request, Uri url)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (url == null) throw new ArgumentNullException(nameof(url));

            return url.IsAbsoluteUri
                ? request.RequestUri.Equals(url)
                : new Uri(request.RequestUri.PathAndQuery, UriKind.Relative).Equals(url);
        }

        #endregion
    }
}