using Web_SOS_Code.Models;

namespace Web_SOS_Code.Services
{
    public class DishService
    {
        private readonly HttpClient _httpClient;

        public DishService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        }

        public string ApiErrorMessage { get; set; } = string.Empty;

        public async Task<List<Dish>> GetDishesAsync()
        {
            var response = await _httpClient.GetAsync("dish");
            if (response.IsSuccessStatusCode)
            {
                var dishes = await response.Content.ReadFromJsonAsync<List<Dish>>();
                return dishes?
                    .ToList() ?? new List<Dish>();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Dish> GetDishByIdAsync(int dishId)
        {
            var response = await _httpClient.GetAsync($"dish/{dishId}");
            if (response.IsSuccessStatusCode)
            {
                var dish = await response.Content.ReadFromJsonAsync<Dish>();
                return dish;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Dish> PostDishAsync(Dish dish)
        {
            var response = await _httpClient.PostAsJsonAsync($"dish", dish);
            if (response.IsSuccessStatusCode)
            {
                var newDish = await response.Content.ReadFromJsonAsync<Dish>();
                return newDish;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Dish> PutDishAsync(int id, Dish dish)
        {
            var response = await _httpClient.PutAsJsonAsync($"dish/{id}", dish);
            if (response.IsSuccessStatusCode)
            {
                var updatedDish = await response.Content.ReadFromJsonAsync<Dish>();
                return updatedDish;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<bool> DeleteDishAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"dish/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }
    }
}
