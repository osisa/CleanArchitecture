// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="MockHttpClient.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;

using BlazorHero.CleanArchitecture.TestInfrastructure.Handlers;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    /// <summary>
    ///     Provides <see cref="HttpClient" /> with a <see cref="MockHttpMessageHandler" /> and a
    ///     <see cref="RequestHistoryHttpMessageHandler" />
    /// </summary>
    /// <seealso cref="System.Net.Http.HttpClient" />
    /// <seealso cref="IWhenable" />
    public class MockHttpClient : HttpClient, IWhenable
    {
        #region Fields

        private readonly MockHttpMessageHandler? _mockHttpMessageHandler;

        private readonly RequestHistoryHttpMessageHandler _requestHistoryHttpMessageHandler;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockHttpClient" /> class.
        /// </summary>
        public MockHttpClient()
            : this(new MockHttpMessageHandler())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockHttpClient" /> class.
        /// </summary>
        /// <param name="fallbackHandler">The handler to use when no rules are matched.</param>
        public MockHttpClient(HttpMessageHandler fallbackHandler)
            : this(new MockHttpMessageHandler(fallbackHandler))
        {
        }

        private MockHttpClient(MockHttpMessageHandler mockHttpMessageHandler)
            : this(new RequestHistoryHttpMessageHandler(mockHttpMessageHandler))
        {
            _mockHttpMessageHandler = mockHttpMessageHandler;
        }

        private MockHttpClient(RequestHistoryHttpMessageHandler mockHttpMessageHandler)
            : base(mockHttpMessageHandler)
        {
            _requestHistoryHttpMessageHandler = mockHttpMessageHandler;
        }

        private MockHttpClient(RequestHistoryHttpMessageHandler historyHttpMessageHandler, MockHttpMessageHandler mockHttpMessageHandler)
            : base(mockHttpMessageHandler)
        {
            _mockHttpMessageHandler = mockHttpMessageHandler;
            _requestHistoryHttpMessageHandler = historyHttpMessageHandler;

            historyHttpMessageHandler.InnerHandler = _mockHttpMessageHandler;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the request history.
        /// </summary>
        /// <value>
        ///     The history.
        /// </value>
        public IReadOnlyList<HttpRequestMessage> RequestHistory => _requestHistoryHttpMessageHandler.History;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Starts creating a rule by specifying the condition when this rule should be applied.
        /// </summary>
        /// <param name="condition">The condition when the rule should be applied.</param>
        /// <returns></returns>
        public IThenable When(Func<HttpRequestMessage, bool> condition)
        {
            return _mockHttpMessageHandler.When(condition);
        }

        #endregion
    }
}