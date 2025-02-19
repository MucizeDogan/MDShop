using MDShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MDShop.WebUI.Services.CatalogServices.BrandServices {
    public interface IBrandService {
        Task<List<ResultBrandDto>> GetAllBrandAsync(bool isAdmin);
        Task CreateBrandAsync(CreateBrandDto createBrandDto);
        Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        Task DeleteBrandAsync(string id);
        Task<UpdateBrandDto> GetByIdBrandAsync(string id);
        Task BrandChangeStatusToTrue(string id);
        Task BrandChangeStatusToFalse(string id);
    }
}
