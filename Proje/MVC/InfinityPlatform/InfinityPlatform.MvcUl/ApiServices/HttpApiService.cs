using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace InfinityPlatform.MvcUI.ApiServices
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> DeleteData(string requestUri, string token = null)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://localhost:5158/api{requestUri}"),
                Headers =
        {
          {HeaderNames.Accept,"application/json" }
        }
            };

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.SendAsync(requestMessage);

            return responseMessage.IsSuccessStatusCode;

        }

        public async Task<T> GetData<T>(string requestUri, string token = null)
        {
            T response = default(T);

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://localhost:5158/api{requestUri}"),
                Headers =
        {
          {HeaderNames.Accept,"application/json" }
        }
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

       

        public async Task<T> PostData<T>(string requestUri, string jsonData, string token = null)
        {
            T response = default(T);

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://localhost:5158/api{requestUri}"),
                Headers =
        {
          {HeaderNames.Accept,"application/json" }
        },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

      
    }
}