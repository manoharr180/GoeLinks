using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GeoLinks.Services.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace GeoLinks.Services.Implementations
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;

        public ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<T> CreateAsync<T>(string url, object data)
        {
            _logger.LogInformation("Making POST request to {Url} with data: {Data}", url, JsonConvert.SerializeObject(data));
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            _logger.LogInformation("Making DELETE request to {Url}", url);
            var response = await _httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public async Task<T> ReadAsync<T>(string url)
        {
            _logger.LogInformation("Making GET request to {Url}", url);
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public async Task<T> UpdateAsync<T>(string url, object data)
        {
            _logger.LogInformation("Making PUT request to {Url} with data: {Data}", url, JsonConvert.SerializeObject(data));
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }
    }
}