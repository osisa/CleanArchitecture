// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="Class2.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;

using Newtonsoft.Json;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    public static class HttpClientExtensions
    {
        #region Public Methods and Operators

        public static T GetAsync<T>(this HttpClient @this, string uriText)
        {
            return @this.GetAsync<T>(new Uri(uriText, UriKind.Relative));
        }

        public static T GetAsync<T>(this HttpClient @this, Uri uri)
        {
            var response = @this.GetAsync(uri).Result;

            //response.EnsureSuccessStatusCode();
            var text = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = text + $"Status:{response.StatusCode}";
                throw new WebException(message);
            }

            return JsonConvert.DeserializeObject<T>(text);
        }


        public static HttpResponseMessage PostAsync<T>(this HttpClient @this, string uriText, T value)
        {
            return @this.PostAsync(new Uri(uriText, UriKind.Relative), value);
        }

        public static HttpResponseMessage PostAsync<T>(this HttpClient @this, Uri uri, T value)
        {
            var data = JsonConvert.SerializeObject(value);

            StringContent httpContent = new (data, System.Text.Encoding.UTF8, "application/json");

            var resp = @this.PostAsync(uri, httpContent).Result;

            return resp;
        }

        public static HttpResponseMessage PutAsync<T>(this HttpClient @this, string uriText, T value)
        {
            return @this.PutAsync(new Uri(uriText, UriKind.Relative), value);
        }

        public static HttpResponseMessage PutAsync<T>(this HttpClient @this, Uri uri, T value)
        {
            var data = JsonConvert.SerializeObject(value);

            StringContent httpContent = new(data, System.Text.Encoding.UTF8, "application/json");

            var resp = @this.PutAsync(uri, httpContent).Result;

            return resp;
        }

        #endregion
    }
}