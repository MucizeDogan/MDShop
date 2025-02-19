using MDShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MDShop.WebUI.Services.CatalogServices.FeatureServices {
    public interface IFeatureService {
        Task<List<ResultFeatureDto>> GetAllFeatureAsync(bool isAdmin);
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
        Task DeleteFeatureAsync(string id);
        Task<UpdateFeatureDto> GetByIdFeatureAsync(string id);
        Task FeatureChangeStatusToTrue(string id);
        Task FeatureChangeStatusToFalse(string id);
    }
}
