using MDShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MDShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _DescriptionProductDetailComponentPartial : ViewComponent{
        private readonly IProductDetailService _productDetailService;

        public _DescriptionProductDetailComponentPartial(IProductDetailService productDetailService) {
            _productDetailService = productDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) {
            var values = await _productDetailService.GetByProductIdProductDetailAsync(id);
            return View(values);
        }
    }
}
