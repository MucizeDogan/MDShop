using MDShop.DtoLayer.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _ReviewProductDetailComponentPartial : ViewComponent {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ReviewProductDetailComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7155/api/Comments/CommentListByProductId/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
