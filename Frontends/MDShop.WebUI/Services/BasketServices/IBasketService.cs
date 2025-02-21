using MDShop.DtoLayer.BasketDtos;

namespace MDShop.WebUI.Services.BasketServices {
    public interface IBasketService {
        Task<BasketTotalDto> GetBasket(string userId);
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task DeleteBasket(string userId);
    }
}
