// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="XmlContent.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Net.Http;
using System.Xml.Serialization;

namespace BlazorHero.CleanArchitecture.Common.Content
{
    /// <summary>
    ///     Represents Xml content.
    /// </summary>
    /// <seealso cref="StringContent" />
    public class XmlContent : StringContent
    {
        #region Static Fields

        private static readonly string DefaultMediaType = "application/xml";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlContent" /> class.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="mediaType">The media type. Defaults to application/xml.</param>
        public XmlContent(object obj, string? mediaType = null)
            : base(ToXmlString(obj), null, mediaType ?? DefaultMediaType)
        {
        }

        #endregion

        #region Methods

        private static string ToXmlString(object? input)
        {
            if (input == null)
                return "";

            using (var writer = new StringWriter())
            {
                new XmlSerializer(input.GetType()).Serialize(writer, input);
                return writer.ToString();
            }
        }

        #endregion
    }
}