using MDShop.Basket.Dtos;
using MDShop.Basket.Settings;
using StackExchange.Redis;
using System.Text.Json;

namespace MDShop.Basket.Services {
    public class BasketService : IBasketService {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService) {
            this._redisService = redisService;
        }

        public async Task DeleteBasket(string userId) {
            await _redisService.GetDb().KeyDeleteAsync(userId); // kullanıcıya göre silme işlemi yapıyoruz. 
        }

        public async Task<BasketTotalDto> GetBasket(string userId) {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId); // Böyle bir sepet olup olmadığını kontrol edecek değişkenim. Kullanıcı id sine göre sepeti getirecek.
            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
        }
        //Verilerinizi Redis'te saklamak ve okumak için StringSetAsync ve StringGetAsync metodları temel işlevlerdir.
        //Redis verilerini JSON olarak saklamak yaygındır ve JsonSerializer ile kolayca dönüştürülebilir.

        public async Task SaveBasket(BasketTotalDto basketTotalDto) {
            await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId, JsonSerializer.Serialize(basketTotalDto)); // key userId, value basketTotalDto
        }
    }
}
