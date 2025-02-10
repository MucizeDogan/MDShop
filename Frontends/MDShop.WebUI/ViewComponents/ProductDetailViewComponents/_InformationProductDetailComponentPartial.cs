using MDShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _InformationProductDetailComponentPartial : ViewComponent {
        private readonly IHttpClientFactory _httpClientFactory;

        public _InformationProductDetailComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/ProductDetails/GetByProductIdProductDetail?id=" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDetailDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
