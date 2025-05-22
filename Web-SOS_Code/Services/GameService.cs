using Web_SOS_Code.Models;

namespace T1_PR2_Client.Services
{
    public class GameService
    {
        private readonly HttpClient _httpClient;

        public GameService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        }

        public string ApiErrorMessage { get; set; } = string.Empty;

        public async Task<List<Dish>> GetGamesAsync()
        {
            var response = await _httpClient.GetAsync("api/games");
            if (response.IsSuccessStatusCode)
            {
                var games = await response.Content.ReadFromJsonAsync<List<Dish>>();
                return games?
                    .ToList() ?? new List<Dish>();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Dish> GetGameByIdAsync(int gameId)
        {
            var response = await _httpClient.GetAsync($"api/games/{gameId}");
            if (response.IsSuccessStatusCode)
            {
                var game = await response.Content.ReadFromJsonAsync<Dish>();
                return game;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<bool> VoteAsync(int gameId, string username)
        {
            var url = $"api/games/vote?gameId={gameId}&userName={username}";
            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

        
    }
}
