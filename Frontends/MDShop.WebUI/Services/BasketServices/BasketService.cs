using MDShop.DtoLayer.BasketDtos;

namespace MDShop.WebUI.Services.BasketServices {
    public class BasketService : IBasketService {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public Task DeleteBasket(string userId) {
            throw new NotImplementedException();
        }

        public Task<BasketTotalDto> GetBasket(string userId) {
            throw new NotImplementedException();
        }

        public Task SaveBasket(BasketTotalDto basketTotalDto) {
            throw new NotImplementedException();
        }
    }
}
