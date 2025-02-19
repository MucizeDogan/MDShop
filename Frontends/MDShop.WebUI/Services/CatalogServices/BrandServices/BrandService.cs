using MDShop.DtoLayer.CatalogDtos.BrandDtos;
using Newtonsoft.Json;

namespace MDShop.WebUI.Services.CatalogServices.BrandServices {
    public class BrandService : IBrandService {
        private readonly HttpClient _httpClient;
        public BrandService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task CreateBrandAsync(CreateBrandDto createBrandDto) {
            var response = await _httpClient.PostAsJsonAsync("brands", createBrandDto);
            if (!response.IsSuccessStatusCode) {
                //var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception("Bu sıra numarasına sahip kayıt zaten mevcut!");
            }
        }
        public async Task DeleteBrandAsync(string id) {
            await _httpClient.DeleteAsync("brands?id=" + id);
        }
        public async Task<UpdateBrandDto> GetByIdBrandAsync(string id) {
            var responseMessage = await _httpClient.GetAsync("brands/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateBrandDto>();
            return values;
        }
        public async Task<List<ResultBrandDto>> GetAllBrandAsync(bool isAdmin = true) {

            string url = $"brands?isadmin={isAdmin.ToString().ToLower()}";
            var responseMessage = await _httpClient.GetAsync(url);

            if (!responseMessage.IsSuccessStatusCode) {
                throw new HttpRequestException($"API isteği başarısız! Status Code: {responseMessage.StatusCode}");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData) ?? new List<ResultBrandDto>();

            // Eğer admin değilse, verileri filtrele
            return isAdmin ? values : values.Where(x => x.Status).OrderBy(x => x.Order).ToList();

        }
        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto) {
            await _httpClient.PutAsJsonAsync<UpdateBrandDto>("brands", updateBrandDto);
        }

        public async Task BrandChangeStatusToTrue(string id) {
            await _httpClient.GetAsync($"brands/brandchangestatustotrue/{id}");
        }

        public async Task BrandChangeStatusToFalse(string id) {
            await _httpClient.GetAsync($"brands/brandchangestatustofalse/{id}");
        }
    }
}
