using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;

namespace MDShop.WebUI.Services.CatalogServices.FeatureSliderServices {
    public interface IFeatureSliderService {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task DeleteFeatureSliderAsync(string id);
        Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string id);
        Task FeatureSliderChageStatusToTrue(string id);
        Task FeatureSliderChageStatusToFalse(string id);
    }
}
