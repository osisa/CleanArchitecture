using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using BlazorHero.CleanArchitecture.Shared.Wrapper;

//using Microsoft.AspNetCore.Mvc.Formatters;

using Newtonsoft.Json;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    public static class HttpClientExtensions
    {

        public const string ApplicationJson = "application/json";

        private static Encoding Encoding => Encoding.UTF8;

        #region Public Methods and Operators

        public static HttpResponseMessage Delete(this HttpClient @this, string uriText)
            => @this.DeleteAsync(CreateUri(uriText)).Result;

        public static TResult Get<TResult>(this HttpClient @this, string uriText)
            => @this.GetAsync<TResult>(uriText).Result;

        public static HttpResponseMessage Post<T>(this HttpClient @this, string uriText, T value)
            => @this.PostAsync(CreateUri(uriText), value).Result;

        public static HttpResponseMessage Put<T>(this HttpClient @this, string uriText, T value)
            => @this.PutAsync(CreateUri(uriText), value).Result;

        public static IResult<T> ToResult<T>(this HttpContent @this)
        {
            var json = @this.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Result<T>>(json);
        }

        #endregion

        #region Methods

        private static Uri CreateUri(string uriText)
            => new(uriText, UriKind.Relative);

        private static Task<TResult> GetAsync<TResult>(this HttpClient @this, string uriText)
            => @this.GetAsync<TResult>(new Uri(uriText, UriKind.Relative));

        private static async Task<TResult> GetAsync<TResult>(this HttpClient @this, Uri uri)
        {
            var response = await @this.GetAsync(uri);

            var text = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = text + $"Status:{response.StatusCode}";
                throw new WebException(message);
            }

            return await Task.FromResult(JsonConvert.DeserializeObject<TResult>(text));
        }

        private static Task<HttpResponseMessage> PostAsync<T>(this HttpClient @this, Uri uri, T value)
            => @this.PostAsync(uri, CreateStringContent(value));
        
        private static Task<HttpResponseMessage> PutAsync<T>(this HttpClient @this, Uri uri, T value)
            => @this.PutAsync(uri, CreateStringContent(value));
        
        private static string SerializeObject<T>(T value) =>  JsonConvert.SerializeObject(value);
        
        private static StringContent CreateStringContent<T>(T value) => new(SerializeObject(value),Encoding, ApplicationJson);

        
        #endregion
    }
}



//public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient @this, string uriText, T value)
//{
//    return @this.PostAsync(new Uri(uriText, UriKind.Relative), value);
//}

//public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient @this, string uriText, T value)
//{
//    return @this.PutAsync(new Uri(uriText, UriKind.Relative), value);
//}