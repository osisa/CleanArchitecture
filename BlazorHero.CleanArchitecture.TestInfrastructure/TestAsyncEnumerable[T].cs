using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        #region Constructors and Destructors

        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        #endregion

        #region Public Properties

        public IQueryProvider Provider => new TestAsyncQueryProvider<T>(this);

        #endregion

        #region Public Methods and Operators

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) => GetEnumerator();//new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TestAsyncEnumerator<T>(((IEnumerable<T>)this).GetEnumerator());
            //return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        
       

        #endregion
    }
}