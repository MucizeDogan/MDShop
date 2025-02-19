using MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MDShop.WebUI.Services.CatalogServices.OfferDiscountServices {
    public interface IOfferDiscountService {
        Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync(bool isAdmin);
        Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        Task DeleteOfferDiscountAsync(string id);
        Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id);
        Task OfferDiscountChangeStatusToTrue(string id);
        Task OfferDiscountChangeStatusToFalse(string id);
    }
}
