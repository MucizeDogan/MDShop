using MDShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MDShop.WebUI.Services.CatalogServices.SpecialOfferServices {
    public interface ISpecialOfferService {
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync(bool isAdmin);
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        Task DeleteSpecialOfferAsync(string id);
        Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
        Task SpecialOfferChangeStatusToTrue(string id);
        Task SpecialOfferChangeStatusToFalse(string id);
    }
}
