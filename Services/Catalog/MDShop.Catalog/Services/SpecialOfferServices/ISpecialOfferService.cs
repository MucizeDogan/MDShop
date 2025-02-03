using MDShop.Catalog.Dtos.SpecialOfferDtos;

namespace MDShop.Catalog.Services.SpecialOfferServices {
    public interface ISpecialOfferService {
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        Task DeleteSpecialOfferAsync(string id);
        Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
        Task SpecialOfferChangeStatusToTrue(string id);
        Task SpecialOfferChangeStatusToFalse(string id);
    }
}
