using MDShop.DtoLayer.CatalogDtos.AboutDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/About")]
    public class AboutController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            AboutViewbagList("Hakkımızda Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Abouts"); //isteğin yapılacağı adres (port-catalog servisine gidecek)
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateAbout")]
        public IActionResult CreateAbout() {
            AboutViewbagList("Yeni Hakkımızda Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7070/api/Abouts", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/Abouts?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateAbout/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id) {
            AboutViewbagList("Hakkımızda Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/Abouts/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateAbout/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/Abouts/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }

        void AboutViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
