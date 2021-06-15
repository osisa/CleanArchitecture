// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="PaginatedResult.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Common.Results
{
    public class PaginatedResult<T> : Result
    {
        #region Constructors and Destructors

        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        internal PaginatedResult(bool succeeded, List<T>? data = default, List<string>? messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        #endregion

        #region Public Properties

        public int CurrentPage { get; set; }

        public List<T> Data { get; set; }

        public bool HasNextPage => CurrentPage < TotalPages;

        public bool HasPreviousPage => CurrentPage > 1;

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        #endregion

        #region Public Methods and Operators

        public static PaginatedResult<T> Failure(List<string> messages)
        {
            return new PaginatedResult<T>(false, default, messages);
        }

        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }

        #endregion
    }
}