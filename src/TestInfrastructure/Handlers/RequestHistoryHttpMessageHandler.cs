// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="RequestHistoryHttpMessageHandler.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Handlers
{
    /// <summary>
    ///     Provides logging of past requests
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public class RequestHistoryHttpMessageHandler : DelegatingHandler
    {
        #region Fields

        private List<HttpRequestMessage> _history = new List<HttpRequestMessage>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestHistoryHttpMessageHandler" /> class.
        /// </summary>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public RequestHistoryHttpMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the request history.
        /// </summary>
        public IReadOnlyList<HttpRequestMessage> History => _history;

        #endregion

        #region Methods

        /// <summary>
        ///     Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _history.Add(request);
            return base.SendAsync(request, cancellationToken);
        }

        #endregion
    }
}