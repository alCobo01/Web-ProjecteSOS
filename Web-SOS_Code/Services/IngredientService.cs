using Web_SOS_Code.Models;

namespace Web_SOS_Code.Services
{
    public class IngredientService
    {
        private readonly HttpClient _httpClient;

        public IngredientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        }

        public string ApiErrorMessage { get; set; } = string.Empty;

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            var response = await _httpClient.GetAsync("ingredient");
            if (response.IsSuccessStatusCode)
            {
                var ingredients = await response.Content.ReadFromJsonAsync<List<Ingredient>>();
                return ingredients?
                    .ToList() ?? new List<Ingredient>();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientID)
        {
            var response = await _httpClient.GetAsync($"ingredient/{ingredientID}");
            if (response.IsSuccessStatusCode)
            {
                var ingredient = await response.Content.ReadFromJsonAsync<Ingredient>();
                return ingredient;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Ingredient> PostIngredientAsync(Ingredient ing)
        {
            var response = await _httpClient.PostAsJsonAsync($"ingredient", ing);
            if (response.IsSuccessStatusCode)
            {
                var newIngredient = await response.Content.ReadFromJsonAsync<Ingredient>();
                return newIngredient;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Ingredient> PutIngredientAsync(int id, Ingredient ing)
        {
            var response = await _httpClient.PutAsJsonAsync($"ingredient/{id}", ing);
            if (response.IsSuccessStatusCode)
            {
                var updatedIngredient = await response.Content.ReadFromJsonAsync<Ingredient>();
                return updatedIngredient;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"ingredient/{id}");
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
