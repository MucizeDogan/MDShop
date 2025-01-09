using MDShop.Catalog.Dtos.CategoryDtos;

namespace MDShop.Catalog.Services.CategoryServices {
    public interface ICategoryService {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(string id);
        Task<GetByIdCategoryDto> GetByIdCategoryAsync();
    }
}
