using MDShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _SliderProductDetailComponentPartial : ViewComponent {
        private readonly IHttpClientFactory _httpClientFactory;

        public _SliderProductDetailComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/ProductImages/ProductImagesByProductId?id=" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdProductImageDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
