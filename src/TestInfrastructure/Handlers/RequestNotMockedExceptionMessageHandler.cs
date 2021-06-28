// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="RequestNotMockedExceptionMessageHandler.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Handlers
{
    /// <summary>
    ///     Provides 404 Not Found default handling
    /// </summary>
    /// <seealso cref="System.Net.Http.HttpMessageHandler" />
    public class NotFoundMessageHandler : HttpMessageHandler
    {
        #region Methods

        /// <summary>
        ///     Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.
        /// </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //using Task.Run to simulate a requset not being instant
            return Task.Run(() => new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        #endregion
    }
}