using MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/OfferDiscount")]
    public class OfferDiscountController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public OfferDiscountController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            OfferDiscountViewbagList("İndirim Teklifi Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/OfferDiscounts?isAdmin=true");
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateOfferDiscount")]
        public IActionResult CreateOfferDiscount() {
            OfferDiscountViewbagList("Yeni İndirim Teklifi Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateOfferDiscount")]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createOfferDiscountDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var res = await client.PostAsync("https://localhost:7070/api/OfferDiscounts", stringContent);

            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
            } else {
                // Hata mesajını API'den okuyoruz.
                var errorMessage = await res.Content.ReadAsStringAsync();

                ModelState.AddModelError("", !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : "Bu sıra numarasına sahip bir kayıt zaten mevcut.");

                return View();
            }
        }

        [Route("DeleteOfferDiscount/{id}")]
        public async Task<IActionResult> DeleteOfferDiscount(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/OfferDiscounts?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateOfferDiscount/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateOfferDiscount(string id) {
            OfferDiscountViewbagList("İndirim Teklifi Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/OfferDiscounts/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateOfferDiscount/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateOfferDiscountDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/OfferDiscounts/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
            }
            return View();
        }

        [Route("OfferDiscountChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> OfferDiscountChangeStatusToTrue(string id) {
            //await _OfferDiscountService.OfferDiscountChageStatusToTrue(id);
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/OfferDiscounts/OfferDiscountChangeStatusToTrue/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
            }
            return View();
        }

        [Route("OfferDiscountChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> OfferDiscountChangeStatusToFalse(string id) {
            //await _OfferDiscountService.OfferDiscountChageStatusToFalse(id);

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/OfferDiscounts/OfferDiscountChangeStatusToFalse/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
            }
            return View();
        }

        void OfferDiscountViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirim Teklifiler";
            ViewBag.v3 = "İndirim Teklifi İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
