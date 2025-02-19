using MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;

namespace MDShop.WebUI.Services.CatalogServices.OfferDiscountServices {
    public class OfferDiscountService : IOfferDiscountService {
        private readonly HttpClient _httpClient;
        public OfferDiscountService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto) {
            await _httpClient.PostAsJsonAsync<CreateOfferDiscountDto>("offerdiscounts", createOfferDiscountDto);
        }
        public async Task DeleteOfferDiscountAsync(string id) {
            await _httpClient.DeleteAsync("offerdiscounts?id=" + id);
        }
        public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id) {
            var responseMessage = await _httpClient.GetAsync("offerdiscounts/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateOfferDiscountDto>();
            return values;
        }
        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync(bool isAdmin = true) {

            string url = $"offerdiscounts?isadmin={isAdmin.ToString().ToLower()}";
            var responseMessage = await _httpClient.GetAsync(url);

            if (!responseMessage.IsSuccessStatusCode) {
                throw new HttpRequestException($"API isteği başarısız! Status Code: {responseMessage.StatusCode}");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData) ?? new List<ResultOfferDiscountDto>();

            // Eğer admin değilse, verileri filtrele
            return isAdmin ? values : values.Where(x => x.Status).OrderBy(x => x.Order).ToList();

        }
        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto) {
            await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("offerdiscounts", updateOfferDiscountDto);
        }

        public async Task OfferDiscountChangeStatusToTrue(string id) {
            await _httpClient.GetAsync($"offerdiscounts/OfferDiscountchangestatustotrue/{id}");
        }

        public async Task OfferDiscountChangeStatusToFalse(string id) {
            await _httpClient.GetAsync($"offerdiscounts/OfferDiscountchangestatustofalse/{id}");
        }
    }
}
