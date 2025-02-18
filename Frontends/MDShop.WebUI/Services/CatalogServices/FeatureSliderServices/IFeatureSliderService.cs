using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;

namespace MDShop.WebUI.Services.CatalogServices.FeatureSliderServices {
    public interface IFeatureSliderService {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync(bool isAdmin);
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task DeleteFeatureSliderAsync(string id);
        Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string id);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
