// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="IResult.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Common.Results
{
    public interface IResult
    {
        #region Public Properties

        List<string> Messages { get; set; }

        bool Succeeded { get; set; }

        #endregion
    }

    public interface IResult<out T> : IResult
    {
        #region Public Properties

        T Data { get; }

        #endregion
    }
}