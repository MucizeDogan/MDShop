﻿using MDShop.DtoLayer.CatalogDtos.AboutDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _FooterUILayoutComponentPartial : ViewComponent {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync() {

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

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
