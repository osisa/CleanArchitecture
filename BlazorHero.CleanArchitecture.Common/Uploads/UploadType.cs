// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="UploadType.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace BlazorHero.CleanArchitecture.Common.Uploads
{
    public enum UploadType
    {
        [Description(@"Images\Products")]
        Product,

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document
    }
}