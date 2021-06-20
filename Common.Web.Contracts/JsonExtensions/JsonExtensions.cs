using System.Text.Json;

namespace Common.Web.Contracts.JsonExtensions
{
    public static class JsonExtensions
    {
        #region Public Methods and Operators

        public static string ToJson<T>(this T value)
        {
            return JsonSerializer.Serialize(value);
        }

        #endregion
    }
}