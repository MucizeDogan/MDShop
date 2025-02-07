using MDShop.DtoLayer.CatalogDtos.AboutDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _FooterUILayoutComponentPartial : ViewComponent {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Abouts");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
