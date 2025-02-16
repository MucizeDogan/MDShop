﻿using MDShop.DtoLayer.CatalogDtos.CategoryDto;

namespace MDShop.WebUI.Services.CatalogServices.CategoryServices {
    public class CategoryService : ICategoryService {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto) {
            await _httpClient.PostAsJsonAsync<CreateCategoryDto>("categories", createCategoryDto);
        }

        public async Task DeleteCategoryAsync(string id) {
            await _httpClient.DeleteAsync("categories?id=" + id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync() {
            var res = await _httpClient.GetAsync("categories");
            var values = await res.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
            return values;
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id) {
            var res = await _httpClient.GetAsync("categories/" + id);
            var value = await res.Content.ReadFromJsonAsync<GetByIdCategoryDto>();
            return value;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto) {
            await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("categories", updateCategoryDto);
        }
    }
}
