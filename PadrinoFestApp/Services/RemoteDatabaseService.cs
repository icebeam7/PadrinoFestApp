using System.Net.Http.Json;
using PadrinoFestApp.Models;
using PadrinoFestApp.Helpers;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

using System.Web;
using System.Net;

namespace PadrinoFestApp.Services
{
    public class RemoteDatabaseApiService : IRemoteDatabaseApiService
    {
        HttpClient client;

        private static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public RemoteDatabaseApiService()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri(Constants.PadrinoFestApiUrl);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<T>> GetItems<T>(string endpoint) where T : new()
        {
            //var response = await client.GetAsync(endpoint);
            var response = await SendApiRequest(HttpMethod.Get, endpoint, null);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<T>>(options);

            return default;
        }

        public async Task<T> GetItem<T>(string endpoint, int id) where T : new()
        {
            endpoint += $"/{id}";

            //var response = await client.GetAsync(endpoint);
            var response = await SendApiRequest(HttpMethod.Get, endpoint, null);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<T>(options);

            return default;
        }

        public async Task<bool> AddItem<T>(string endpoint, T item) where T : new()
        {
            var body = JsonSerializer.Serialize(item);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            //var response = await client.PostAsync(endpoint, content);
            var response = await SendApiRequest(HttpMethod.Post, endpoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddItems<T>(string endpoint, List<T> items) where T : new()
        {
            var body = JsonSerializer.Serialize(items);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            //var response = await client.PostAsync(endpoint, content);
            var response = await SendApiRequest(HttpMethod.Post, endpoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItem<T>(string endpoint, int id, T item) where T : new()
        {
            endpoint += $"/{id}";

            var body = JsonSerializer.Serialize(item);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            //var response = await client.PutAsync(endpoint, content);
            var response = await SendApiRequest(HttpMethod.Put, endpoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItem<T>(string endpoint, int id) where T : new()
        {
            endpoint += $"/{id}";

            //var response = await client.DeleteAsync(endpoint);
            var response = await SendApiRequest(HttpMethod.Delete, endpoint, null);
            return response.IsSuccessStatusCode;
        }

        private async Task<HttpResponseMessage> SendApiRequest(
            HttpMethod method, string controller, HttpContent content, bool tryAgain = true)
        {
            var message = new HttpRequestMessage(method, controller);

            if (content != null)
                message.Content = content;

            var response = await client.SendAsync(message);
            return response;
        }

    }
}
