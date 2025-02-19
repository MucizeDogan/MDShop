using MDShop.DtoLayer.CatalogDtos.CategoryDto;
using MDShop.DtoLayer.CatalogDtos.ProductDtos;
using MDShop.WebUI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _NavbarUILAyoutComponentPartial : ViewComponent{
        private readonly ICategoryService _categoryService;

        public _NavbarUILAyoutComponentPartial(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            try {
                var values = await _categoryService.GetAllCategoryAsync();
                return View(values);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
