using MDShop.DtoLayer.DiscountDtos;

namespace MDShop.WebUI.Services.DiscountServices {
    public interface IDiscountService {
        Task<GetDiscountCodeDetailByCode> GetDiscountCode(string code);
        Task<int> GetDiscountCouponCountRate(string code);
    }
}
