using MDShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MDShop.WebUI.Services.CatalogServices.ProductImagesServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _SliderProductDetailComponentPartial : ViewComponent {
        private readonly IProductImageService _productImageService;

        public _SliderProductDetailComponentPartial(IProductImageService productImageService) {
            _productImageService = productImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) {
            var values = await _productImageService.GetByProductIdProductImageAsync(id);
            return View(values);
        }
    }
}
