using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BlazorHero.CleanArchitecture.Shared.Wrapper;

using Newtonsoft.Json;

namespace BlazorHero.CleanArchitecture.TestInfrastructure
{
    public static class HttpClientExtensions
    {
        #region Public Methods and Operators

        public static TResult Get<TResult>(this HttpClient @this, string uriText)
        {
            return @this.GetAsync<TResult>(uriText).Result;
        }

        public static Task<TResult> GetAsync<TResult>(this HttpClient @this, string uriText)
        {
            return @this.GetAsync<TResult>(new Uri(uriText, UriKind.Relative));
        }

        public static Task<TResult> GetAsync<TResult>(this HttpClient @this, Uri uri)
        {
            var response = @this.GetAsync(uri).Result;

            //response.EnsureSuccessStatusCode();
            var text = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = text + $"Status:{response.StatusCode}";
                throw new WebException(message);
            }

            return Task.FromResult(JsonConvert.DeserializeObject<TResult>(text));
        }

        public static HttpResponseMessage Post<T>(this HttpClient @this, string uriText, T value)
        {
            return @this.PostAsync(new Uri(uriText, UriKind.Relative), value).Result;
        }

        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient @this, string uriText, T value)
        {
            return @this.PostAsync(new Uri(uriText, UriKind.Relative), value);
        }

        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient @this, Uri uri, T value)
        {
            var data = JsonConvert.SerializeObject(value);

            StringContent httpContent = new(data, Encoding.UTF8, "application/json");

            var resp = @this.PostAsync(uri, httpContent);

            return resp;
        }

        public static HttpResponseMessage Put<T>(this HttpClient @this, string uriText, T value)
        {
            return @this.PutAsync(new Uri(uriText, UriKind.Relative), value).Result;
        }

        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient @this, string uriText, T value)
        {
            return @this.PutAsync(new Uri(uriText, UriKind.Relative), value);
        }

        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient @this, Uri uri, T value)
        {
            var data = JsonConvert.SerializeObject(value);

            StringContent httpContent = new(data, Encoding.UTF8, "application/json");

            var resp = @this.PutAsync(uri, httpContent);

            return resp;
        }

        public static HttpResponseMessage Delete(this HttpClient @this, Uri uri)
        {
         
            var resp = @this.DeleteAsync(uri).Result;

            return resp;
        }

        public static IResult<T> ToResult<T>(this HttpContent @this)
        {
            var json = @this.ReadAsStringAsync().Result;
            var value = JsonConvert.DeserializeObject<Result<T>>(json);

            return value;
        }

        #endregion
    }
}