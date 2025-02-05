using MDShop.Catalog.Dtos.FeatureSliderDtos;

namespace MDShop.Catalog.Services.FeatureSliderServices {
    public interface IFeatureSliderService {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync(bool isAdmin);
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task DeleteFeatureSliderAsync(string id);
        Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
