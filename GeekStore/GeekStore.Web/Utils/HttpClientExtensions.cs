using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekStore.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>
            (this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) 
                throw new ApplicationException($"Something went wrong calling the API: " +
                    $"{response.ReasonPhrase}");

            var DataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(DataAsString,
                new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });

        }

        public static Task<HttpResponseMessage> PostAsJson<T> (this HttpClient httpClient, string Url, T Data)
        {
            var DataAsString = JsonSerializer.Serialize(Data);
            var content = new StringContent(DataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(Url, content);
        }
        public static Task<HttpResponseMessage> PutAsJson<T> (this HttpClient httpClient, string Url, T Data)
        {
            var DataAsString = JsonSerializer.Serialize(Data);
            var content = new StringContent(DataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(Url, content);
        }
    }
}
