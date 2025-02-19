using MDShop.DtoLayer.CatalogDtos.ProductDtos;
using MDShop.WebUI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _FeatureProductDetailComponentPartial : ViewComponent {
        private readonly IProductService _productService;

        public _FeatureProductDetailComponentPartial(IProductService productService) {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) {
         var values = await _productService.GetByIdProductAsync(id);
            return View(values);
        }
    }
}
