using MDShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MDShop.WebUI.Services.CatalogServices.FeatureServices {
    public class FeatureService : IFeatureService {
        private readonly HttpClient _httpClient;
        public FeatureService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto) {
            await _httpClient.PostAsJsonAsync<CreateFeatureDto>("features", createFeatureDto);
        }
        public async Task DeleteFeatureAsync(string id) {
            await _httpClient.DeleteAsync("features?id=" + id);
        }
        public async Task<UpdateFeatureDto> GetByIdFeatureAsync(string id) {
            var responseMessage = await _httpClient.GetAsync("features/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>();
            return values;
        }
        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync(bool isAdmin = true) {

            string url = $"features?isadmin={isAdmin.ToString().ToLower()}";
            var responseMessage = await _httpClient.GetAsync(url);

            if (!responseMessage.IsSuccessStatusCode) {
                throw new HttpRequestException($"API isteği başarısız! Status Code: {responseMessage.StatusCode}");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData) ?? new List<ResultFeatureDto>();

            // Eğer admin değilse, verileri filtrele
            return isAdmin ? values : values.Where(x => x.Status).OrderBy(x => x.Order).ToList();

        }
        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto) {
            await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("features", updateFeatureDto);
        }

        public async Task FeatureChangeStatusToTrue(string id) {
            await _httpClient.GetAsync($"features/featurechangestatustotrue/{id}");
        }

        public async Task FeatureChangeStatusToFalse(string id) {
            await _httpClient.GetAsync($"features/featurechangestatustofalse/{id}");
        }
    }
}
