using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider, IQueryable<TEntity>
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var innerResult = _inner.Execute<TEntity>(expression);
            return (TResult)(object)Task.FromResult(innerResult);
                
            //var result= _inner.Execute<TResult>(expression);

            //return result;
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            return Execute<TResult>(expression);
        }

        public IEnumerator<TEntity> GetEnumerator() => (IEnumerator<TEntity>)new TEntity[]{}.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Type ElementType => typeof(TEntity);

        public Expression Expression
        {
            get
            {
                var x = _inner as IQueryable<TEntity>;

                return x.Expression;
            }
        }

        public IQueryProvider Provider => this; //new TestAsyncQueryProvider<TEntity>(_inner);
    }
}