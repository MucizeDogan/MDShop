using MDShop.DtoLayer.CatalogDtos.CategoryDto;
using MDShop.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _NavbarUILAyoutComponentPartial : ViewComponent{
        private readonly IHttpClientFactory _httpClientFactory;
        public _NavbarUILAyoutComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            #region Postman de elde ettiğimiz token ı manuel olarak uı da göstermek için buradan istekta bulunuyoruz

            string token = "";
            using (var httpClient = new HttpClient()) {
                var request = new HttpRequestMessage {
                    RequestUri = new Uri("http://localhost:5001/connect/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","MDShopVisitorId" },
                        {"client_secret","mdshopsecret" },
                        {"grant_type","client_credentials" }
                    })
                };

                using (var response = await httpClient.SendAsync(request)) {
                    if (response.IsSuccessStatusCode) {
                        var content = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JObject.Parse(content);
                        token = tokenResponse["access_token"].ToString();
                    }
                }
            }
            #endregion

            // Bu regionda gelen tokenı okutuyoruz

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // BU ÖNEMLİİİİİ postmandeki Authorization kısmındaki Header Prefix i girmiş oluyoruz

            var res = await client.GetAsync("https://localhost:7070/api/Categories");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
