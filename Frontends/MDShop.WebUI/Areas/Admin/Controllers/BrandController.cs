using MDShop.DtoLayer.CatalogDtos.BrandDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            BrandViewbagList("Marka Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Brands?isAdmin=true");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateBrand")]
        public IActionResult CreateBrand() {
            BrandViewbagList("Yeni Marka Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBrandDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:7070/api/Brands", stringContent);

            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            } else {
                // Hata mesajını API'den okuyoruz.
                var errorMessage = await res.Content.ReadAsStringAsync();

                ModelState.AddModelError("", !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : "Bu sıra numarasına sahip bir kayıt zaten mevcut.");

                return View();
            }
        }

        [Route("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/Brands?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateBrand/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id) {
            BrandViewbagList("Marka Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Brands/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBrandDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateBrand/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBrandDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/Brands/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            }
            return View();
        }

        [Route("BrandChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> BrandChangeStatusToTrue(string id) {
            //await _BrandService.BrandChageStatusToTrue(id);
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Brands/BrandChangeStatusToTrue/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            }
            return View();
        }

        [Route("BrandChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> BrandChangeStatusToFalse(string id) {
            //await _BrandService.BrandChageStatusToFalse(id);

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Brands/BrandChangeStatusToFalse/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            }
            return View();
        }

        void BrandViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Markalar";
            ViewBag.v3 = "Marka İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
