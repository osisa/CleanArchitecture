// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="IUploadService.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace BlazorHero.CleanArchitecture.Common.Uploads
{
    public interface IUploadService
    {
        #region Public Methods and Operators

        string UploadAsync(UploadRequest request);

        #endregion
    }
}