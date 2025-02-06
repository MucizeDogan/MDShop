using MDShop.DtoLayer.CatalogDtos.FeatureDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class FeatureController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index() {
            FeatureViewbagList("Öne Çıkan Alan Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Features?isAdmin=true");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateFeature")]
        public IActionResult CreateFeature() {
            FeatureViewbagList("Yeni Öne Çıkan Alan Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateFeature")]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto) {
            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //var res = await client.PostAsync("https://localhost:7070/api/Features", stringContent);
            //if (res.IsSuccessStatusCode) {
            //    return RedirectToAction("Index", "Feature", new { area = "Admin" });
            //}
            //return View();

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:7070/api/Features", stringContent);

            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            } else {
                // Hata mesajını API'den okuyoruz.
                var errorMessage = await res.Content.ReadAsStringAsync();

                ModelState.AddModelError("", !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : "Bu sıra numarasına sahip bir kayıt zaten mevcut.");

                return View();
            }
        }

        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/Features?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateFeature/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id) {
            FeatureViewbagList("Öne Çıkan Alan Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Features/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/Features/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("FeatureChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureChangeStatusToTrue(string id) {
            //await _FeatureService.FeatureChageStatusToTrue(id);
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Features/FeatureChangeStatusToTrue/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("FeatureChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureChangeStatusToFalse(string id) {
            //await _FeatureService.FeatureChageStatusToFalse(id);

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Features/FeatureChangeStatusToFalse/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        void FeatureViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan Alanlar";
            ViewBag.v3 = "Öne Çıkan Alan İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
