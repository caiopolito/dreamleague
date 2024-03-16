using dreamleague.shared.Properties;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace dreamleague.shared.Infrastructures
{
    public abstract class GenericHttpRepository
    {
        private readonly HttpClient _httpClient;

        public GenericHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<T> GetAsync<T>(string url, Dictionary<string, string>? pathParams = null)
        {
            if (pathParams != null)
                url = QueryHelpers.AddQueryString(url, pathParams);
            var uri = new Uri(url, UriKind.Relative);
            using var response = await _httpClient.GetAsync(uri);
            return await HandleResponse<T>(response);
        }

        protected async Task<T> PostAsync<T>(string url, object request)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var content = GetContent(request);
            using var response = await _httpClient.PostAsync(uri, content);
            return await HandleResponse<T>(response);
        }

        protected async Task<T> PutAsync<T>(string url, object request)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var content = GetContent(request);
            using var response = await _httpClient.PutAsync(uri, content);
            return await HandleResponse<T>(response);
        }


        protected async Task DeleteAsync(string url)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var response = await _httpClient.DeleteAsync(uri);
            await HandleResponse(response);
        }

        private static StringContent GetContent(object request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        protected async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception(Resources.ExternalServerError, new Exception(await response.Content.ReadAsStringAsync()));
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
        protected Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception(Resources.ExternalServerError);
            return Task.CompletedTask;
        }
    }
}
