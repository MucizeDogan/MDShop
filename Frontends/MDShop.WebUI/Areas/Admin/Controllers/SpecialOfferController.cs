using MDShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/SpecialOffer")]
    public class SpecialOfferController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public SpecialOfferController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            SpecialOfferViewbagList("Özel Teklif Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/SpecialOffers?isAdmin=true");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateSpecialOffer")]
        public IActionResult CreateSpecialOffer() {
            SpecialOfferViewbagList("Yeni Özel Teklif Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateSpecialOffer")]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto) {
            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(createSpecialOfferDto);
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //var res = await client.PostAsync("https://localhost:7070/api/SpecialOffers", stringContent);
            //if (res.IsSuccessStatusCode) {
            //    return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            //}
            //return View();

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSpecialOfferDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:7070/api/SpecialOffers", stringContent);

            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            } else {
                // Hata mesajını API'den okuyoruz.
                var errorMessage = await res.Content.ReadAsStringAsync();

                ModelState.AddModelError("", !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : "Bu sıra numarasına sahip bir kayıt zaten mevcut.");

                return View();
            }
        }

        [Route("DeleteSpecialOffer/{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/SpecialOffers?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id) {
            SpecialOfferViewbagList("Özel Teklif Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/SpecialOffers/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateSpecialOfferDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/SpecialOffers/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }

        [Route("SpecialOfferChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> SpecialOfferChangeStatusToTrue(string id) {
            //await _SpecialOfferService.SpecialOfferChageStatusToTrue(id);
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/SpecialOffers/SpecialOfferChangeStatusToTrue/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }

        [Route("SpecialOfferChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> SpecialOfferChangeStatusToFalse(string id) {
            //await _SpecialOfferService.SpecialOfferChageStatusToFalse(id);

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/SpecialOffers/SpecialOfferChangeStatusToFalse/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }

        void SpecialOfferViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
