using MDShop.DtoLayer.CatalogDtos.CategoryDto;
using MDShop.WebUI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _CategoriesDefaultComponentPartial : ViewComponent {
        private readonly ICategoryService _categoryService;

        public _CategoriesDefaultComponentPartial(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}
