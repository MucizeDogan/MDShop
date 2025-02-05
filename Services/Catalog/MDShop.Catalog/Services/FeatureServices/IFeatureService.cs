using MDShop.Catalog.Dtos.FeatureDtos;
using MDShop.Catalog.Dtos.SpecialOfferDtos;

namespace MDShop.Catalog.Services.FeatureServices {
    public interface IFeatureService {
        Task<List<ResultFeatureDto>> GetAllFeatureAsync(bool isAdmin);
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
        Task DeleteFeatureAsync(string id);
        Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id);
        Task FeatureChangeStatusToTrue(string id);
        Task FeatureChangeStatusToFalse(string id);
    }
}
