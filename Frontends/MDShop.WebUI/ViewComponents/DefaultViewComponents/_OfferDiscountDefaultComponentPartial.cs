using MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _OfferDiscountDefaultComponentPartial : ViewComponent{
        private readonly IHttpClientFactory _httpClientFactory;

        public _OfferDiscountDefaultComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/OfferDiscounts?isAdmin=false");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
