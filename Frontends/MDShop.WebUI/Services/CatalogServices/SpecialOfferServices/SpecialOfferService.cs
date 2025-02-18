using MDShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;

namespace MDShop.WebUI.Services.CatalogServices.SpecialOfferServices {
    public class SpecialOfferService : ISpecialOfferService {
        private readonly HttpClient _httpClient;

        public SpecialOfferService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto) {
            await _httpClient.PostAsJsonAsync<CreateSpecialOfferDto>("specialoffers", createSpecialOfferDto);
        }

        public async Task DeleteSpecialOfferAsync(string id) {
            await _httpClient.DeleteAsync("specialoffers?id=" + id);
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync(bool isAdmin = true) {
            if (isAdmin) {
                var responseMessage = await _httpClient.GetAsync("specialoffers?isadmin=true");
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
                return values;
            } else {
                var responseMessage = await _httpClient.GetAsync("specialoffers?isadmin=false");
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
                var filteredValues = values.Where(x => x.Status).OrderBy(x => x.Order).ToList();

                return filteredValues;
            }
            
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto) {
            await _httpClient.PutAsJsonAsync<UpdateSpecialOfferDto>("specialoffers", updateSpecialOfferDto);
        }

        public async Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id) {
            var res = await _httpClient.GetAsync("specialoffers/" + id);
            var value = await res.Content.ReadFromJsonAsync<UpdateSpecialOfferDto>();
            return value;
        }

        public async Task SpecialOfferChangeStatusToFalse(string id) {
            await _httpClient.GetAsync($"specialoffers/specialofferchangestatustofalse/{id}");
        }

        public async Task SpecialOfferChangeStatusToTrue(string id) {
            await _httpClient.GetAsync($"specialoffers/specialofferchangestatustotrue/{id}");
        }
    }
}
