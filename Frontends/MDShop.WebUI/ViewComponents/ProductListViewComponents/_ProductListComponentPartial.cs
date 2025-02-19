using MDShop.DtoLayer.CatalogDtos.ProductDtos;
using MDShop.WebUI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductListViewComponents {
    public class _ProductListComponentPartial : ViewComponent {
        private readonly IProductService _productService;

        public _ProductListComponentPartial(IProductService productService) {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) {
            //var client = _httpClientFactory.CreateClient();
            //var res = await client.GetAsync("https://localhost:7070/api/Products/GetProductsWithCategoryByCategoryId?id=" + id);
            //if (res.IsSuccessStatusCode) {
            //    var jsonData = await res.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
            //    return View(values);
            //}

            //return View();

            var values = await _productService.GetProductsWithCategoryByCatetegoryIdAsync(id);
            return View(values);
        }
    }
}
