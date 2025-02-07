using MDShop.Catalog.Dtos.BrandDtos;

namespace MDShop.Catalog.Services.BrandServices {
    public interface IBrandService {
        Task<List<ResultBrandDto>> GetAllBrandAsync(bool isAdmin);
        Task CreateBrandAsync(CreateBrandDto createBrandDto);
        Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        Task DeleteBrandAsync(string id);
        Task<GetByIdBrandDto> GetByIdBrandAsync(string id);
        Task BrandChangeStatusToTrue(string id);
        Task BrandChangeStatusToFalse(string id);
    }
}
