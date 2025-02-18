using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MDShop.WebUI.Services.CatalogServices.FeatureSliderServices {
    public class FeatureSliderService : IFeatureSliderService {
        private readonly HttpClient _httpClient;
        public FeatureSliderService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto) {
            await _httpClient.PostAsJsonAsync<CreateFeatureSliderDto>("featuresliders", createFeatureSliderDto);
        }
        public async Task DeleteFeatureSliderAsync(string id) {
            await _httpClient.DeleteAsync("featuresliders?id=" + id);
        }
        public async Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string id) {
            var responseMessage = await _httpClient.GetAsync("featuresliders/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>();
            return values;
        }
        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync(bool isAdmin = true) {
            //if (isAdmin) {
            //    var responseMessage = await _httpClient.GetAsync("featuresliders?isadmin=true");
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            //    return values;
            //} else {
            //    var responseMessage = await _httpClient.GetAsync("featuresliders?isadmin=false");
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            //    var filteredValues = values.Where(x => x.Status).OrderBy(x => x.Order).ToList();

            //    return filteredValues;
            //}

            string url = $"featuresliders?isadmin={isAdmin.ToString().ToLower()}";
            var responseMessage = await _httpClient.GetAsync(url);

            if (!responseMessage.IsSuccessStatusCode) {
                throw new HttpRequestException($"API isteği başarısız! Status Code: {responseMessage.StatusCode}");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData) ?? new List<ResultFeatureSliderDto>();

            // Eğer admin değilse, verileri filtrele
            return isAdmin ? values : values.Where(x => x.Status).OrderBy(x => x.Order).ToList();

        }
        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto) {
            await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("featuresliders", updateFeatureSliderDto);
        }

        public async Task FeatureSliderChangeStatusToTrue(string id) {
            await _httpClient.GetAsync($"featuresliders/featuresliderchangestatustotrue/{id}");
        }

        public async Task FeatureSliderChangeStatusToFalse(string id) {
            await _httpClient.GetAsync($"featuresliders/featuresliderchangestatustofalse/{id}");
        }
    }
}
