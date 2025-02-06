using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _SliderDefaultComponentPartial : ViewComponent{
        private readonly IHttpClientFactory _httpClientFactory;

        public _SliderDefaultComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/FeatureSliders?isAdmin=false"); //isteğin yapılacağı adres (port-catalog servisine gidecek)
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
                var trueValues = values.Where(x => x.Status == true).ToList();
                return View(trueValues);
            }

            return View();
        }
    }
}
