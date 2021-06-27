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
    public class TestAsyncQueryProvider2<TEntity> : IAsyncQueryProvider, IQueryable<TEntity>
    {
        private readonly IQueryable<TEntity> _inner;

        public TestAsyncQueryProvider2(IQueryable<TEntity> inner)
        {
            _inner = inner;
        }

        public Type ElementType => _inner.ElementType;

        public Expression Expression => _inner.Expression;

        public IQueryProvider Provider => _inner.Provider;

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_inner).GetEnumerator();
        }

        //public IQueryable CreateQuery(Expression expression)
        //{
        //    return new TestAsyncEnumerable<TEntity>(expression);
        //}

        //public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        //{
        //    return new TestAsyncEnumerable<TElement>(expression);
        //}

        //public object Execute(Expression expression)
        //{
        //    return _inner.Execute(expression);
        //}

        //public TResult Execute<TResult>(Expression expression)
        //{
        //    return _inner.Execute<TResult>(expression);
        //}

        //public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        //{
        //    return new TestAsyncEnumerable<TResult>(expression);
        //}

        //public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(Execute<TResult>(expression));
        //}

        //TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        //{
        //    return Execute<TResult>(expression);
        //}

        //public IEnumerator<TEntity> GetEnumerator() => throw new NotImplementedException();

        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        //public Type ElementType { get; }

        //public Expression Expression { get; }

        //public IQueryProvider Provider { get; }
        public IQueryable CreateQuery(Expression expression) => throw new NotImplementedException();

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => throw new NotImplementedException();

        public object? Execute(Expression expression) => throw new NotImplementedException();

        public TResult Execute<TResult>(Expression expression) => throw new NotImplementedException();

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();
    }
}