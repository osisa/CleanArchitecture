// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="JsonContent.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

using System.Text.Json;

using Newtonsoft.Json;

namespace BlazorHero.CleanArchitecture.TestInfrastructure.Content
{
    /// <summary>
    ///     Represents Json content.
    /// </summary>
    /// <seealso cref="StringContent" />
    public class JsonContent : StringContent
    {
        #region Static Fields

        private static readonly string DefaultMediaType = "application/json";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonContent" /> class.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="mediaType">The media type. Defaults to application/json</param>
        /// <param name="settings">The json serializer settings.</param>
        public JsonContent(object obj, string? mediaType = null, JsonSerializerSettings? settings = null)
            : base(JsonConvert.SerializeObject(obj, settings), null, mediaType ?? DefaultMediaType)
        {
        }

        #endregion
    }
}