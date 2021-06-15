// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="UploadRequest.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace BlazorHero.CleanArchitecture.Common.Uploads
{
    public class UploadRequest
    {
        #region Public Properties

        public byte[] Data { get; set; }

        public string Extension { get; set; }

        public string FileName { get; set; }

        public UploadType UploadType { get; set; }

        #endregion
    }
}