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
        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync() {
            var responseMessage = await _httpClient.GetAsync("featuresliders");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            return values;
        }
        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto) {
            await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("featuresliders", updateFeatureSliderDto);
        }

        public async Task FeatureSliderChageStatusToTrue(string id) {
            var responseMessage = await _httpClient.GetAsync("featuresliders/" + id);
            if (responseMessage.IsSuccessStatusCode) {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>();
                if (value != null) {
                    value.Status = true;
                    await _httpClient.PutAsJsonAsync("featuresliders", value);
                }
            }

        }

        public async Task FeatureSliderChageStatusToFalse(string id) {
            var responseMessage = await _httpClient.GetAsync("featuresliders/" + id);
            if (responseMessage.IsSuccessStatusCode) {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>();
                if (value != null) {
                    value.Status = false;
                    await _httpClient.PutAsJsonAsync("featuresliders", value);
                }
            }
        }
    }
}
