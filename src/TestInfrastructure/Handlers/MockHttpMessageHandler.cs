// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="MockHttpMessageHandler.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Handlers
{
    /// <summary>
    ///     Provides http handling based on configurable rules.
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    /// <seealso cref="IWhenable" />
    public class MockHttpMessageHandler : DelegatingHandler, IWhenable
    {
        #region Fields

        private  ReaderWriterLockSlim _rulesLock = new ReaderWriterLockSlim();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockHttpMessageHandler" /> class.
        /// </summary>
        /// <param name="fallbackHandler">The handler to use when no rules are matched.</param>
        public MockHttpMessageHandler(HttpMessageHandler fallbackHandler)
            : base(fallbackHandler)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockHttpMessageHandler" /> class.
        /// </summary>
        public MockHttpMessageHandler()
            : this(new NotFoundMessageHandler())
        {
        }

        #endregion

        #region Properties

        private Stack<Rule> _rules { get; set; } = new Stack<Rule>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Starts creating a rule by specifying the condition when this rule should be applied.
        /// </summary>
        /// <param name="condition">The condition when the rule should be applied.</param>
        /// <returns></returns>
        public IThenable When(Func<HttpRequestMessage, bool> condition)
        {
            var rule = new Rule() { Condition = condition };
            _rulesLock.EnterWriteLock();
            try
            {
                this._rules.Push(rule);
            }
            finally
            {
                _rulesLock.ExitWriteLock();
            }

            return rule;
        }

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
            Rule rule;
            _rulesLock.EnterReadLock();
            try
            {
                rule = _rules.FirstOrDefault(x => x.IsRuleComplete && x.Condition(request));
            }
            finally
            {
                _rulesLock.ExitReadLock();
            }

            return rule != null
                ? rule.ResponseFactory(request)
                : base.SendAsync(request, cancellationToken);
        }

        #endregion

        private class Rule : IWhenable, IThenable
        {
            #region Public Properties

            public Func<HttpRequestMessage, bool>? Condition { get; set; }

            public bool IsRuleComplete => Condition != null && ResponseFactory != null;

            public Func<HttpRequestMessage, Task<HttpResponseMessage>>? ResponseFactory { get; set; }

            #endregion

            #region Public Methods and Operators

            public void Then(Func<HttpRequestMessage, Task<HttpResponseMessage>> resultFactory)
            {
                ResponseFactory = resultFactory;
            }

            public IThenable When(Func<HttpRequestMessage, bool> condition)
            {
                var oldCondition = Condition;
                Condition = req => oldCondition(req) && condition(req);

                return this;
            }

            #endregion
        }
    }
}