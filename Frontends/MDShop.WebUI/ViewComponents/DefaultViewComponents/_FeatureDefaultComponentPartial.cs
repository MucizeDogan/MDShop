using MDShop.DtoLayer.CatalogDtos.FeatureDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _FeatureDefaultComponentPartial : ViewComponent {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FeatureDefaultComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<IViewComponentResult> InvokeAsync() {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Features?isAdmin=false");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
