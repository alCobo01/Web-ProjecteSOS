using Web_SOS_Code.Models;
using Web_SOS_Code.Models.Menu;

namespace Web_SOS_Code.Controllers
{
    public class MenuService
    {
        private readonly HttpClient _httpClient;

        public MenuService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        }

        public string ApiErrorMessage { get; set; } = string.Empty;

        public async Task<List<MenuDish>> GetMenuAsync()
        {
            var response = await _httpClient.GetAsync("menu");
            if (response.IsSuccessStatusCode)
            {
                var dishes = await response.Content.ReadFromJsonAsync<List<MenuDish>>();
                return dishes?
                    .ToList() ?? new List<MenuDish>();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }
    }
}
