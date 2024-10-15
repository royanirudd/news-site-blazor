using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using NewsApp.Models;

namespace NewsApp
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "5a98534ebfc24fe09730f5f88684aecf";
        private const string BaseUrl = "https://newsapi.org/v2/";

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
        }

        public async Task<List<Article>> GetNewsArticlesAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}top-headlines?country=us");
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Raw JSON: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API returned {response.StatusCode}: {content}");
                }

                var newsApiResponse = JsonSerializer.Deserialize<NewsApiResponse>(content, options);
                return newsApiResponse?.Articles ?? new List<Article>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetNewsArticlesAsync: {ex.Message}");
                throw;
            }
        }
    }

    public class NewsApiResponse
    {
        public string? Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article>? Articles { get; set; }
    }
}
