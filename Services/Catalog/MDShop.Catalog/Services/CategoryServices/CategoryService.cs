using AutoMapper;
using MDShop.Catalog.Dtos.CategoryDtos;
using MDShop.Catalog.Entities;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.CategoryServices {
    public class CategoryService : ICategoryService {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public Task CreateCategoryAsync(CreateCategoryDto createCategoryDto) {
            throw new NotImplementedException();
        }

        public Task<List<ResultCategoryDto>> GetAllCategoryAsync() {
            throw new NotImplementedException();
        }

        public Task<GetByIdCategoryDto> GetByIdCategoryAsync() {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(string id) {
            throw new NotImplementedException();
        }
    }
}
