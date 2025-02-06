using MDShop.Catalog.Dtos.OfferDiscountDtos;

namespace MDShop.Catalog.Services.OfferDiscountServices {
    public interface IOfferDiscountService {
        Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync(bool isAdmin);
        Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        Task DeleteOfferDiscountAsync(string id);
        Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id);
        Task OfferDiscountChangeStatusToTrue(string id);
        Task OfferDiscountChangeStatusToFalse(string id);
    }
}
