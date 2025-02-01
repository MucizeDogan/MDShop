using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MDShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/FeatureSlider")]
    public class FeatureSliderController : Controller {
        //private readonly IFeatureSliderService _featureSliderService;
        //public FeatureSliderController(IFeatureSliderService featureSliderService) {
        //    _featureSliderService = featureSliderService;
        //}

        //[Route("Index")]
        //public async Task<IActionResult> Index() {
        //    FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Listesi");
        //    var values = await _featureSliderService.GetAllFeatureSliderAsync();
        //    return View(values);
        //}

        //[HttpGet]
        //[Route("CreateFeatureSlider")]
        //public IActionResult CreateFeatureSlider() {
        //    FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Ekleme");
        //    return View();
        //}

        //[HttpPost]
        //[Route("CreateFeatureSlider")]
        //public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto) {
        //    await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
        //    return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        //}

        //[Route("DeleteFeatureSlider/{id}")]
        //public async Task<IActionResult> DeleteFeatureSlider(string id) {
        //    await _featureSliderService.DeleteFeatureSliderAsync(id);
        //    return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        //}

        //[Route("UpdateFeatureSlider/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> UpdateFeatureSlider(string id) {
        //    FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Güncelleme");
        //    var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
        //    return View(values);
        //}

        //[Route("UpdateFeatureSlider/{id}")]
        //[HttpPost]
        //public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto) {

        //    await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
        //    return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        //}

        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureSliderController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Listesi");

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/FeatureSliders"); //isteğin yapılacağı adres (port-catalog servisine gidecek)
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateFeatureSlider")]
        public IActionResult CreateFeatureSlider() {
            FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateFeatureSlider")]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureSliderDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://localhost:7070/api/FeatureSliders", stringContent);
            // Parametre olarak gönderilen değeri (createFeatureSliderDto) aldık json a dönüştürdük. Sonra bu değeri content olarak atadık. Neyin content olarak atandığı -> jsonData, bu atanan değerin dil desteği -> Encoding.UTF8, bunun mediatr ü ne olduğu -> application/json
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id) {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("https://localhost:7070/api/FeatureSliders?id=" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id) {
            FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Güncelleme");
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/FeatureSliders/" + id);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto) {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var res = await client.PutAsync("https://localhost:7070/api/FeatureSliders/", stringContent);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        [Route("FeatureSliderChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureSliderChangeStatusToTrue(string id) {
            //await _featureSliderService.FeatureSliderChageStatusToTrue(id);
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/FeatureSliders/FeatureSliderChangeStatusToTrue/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        [Route("FeatureSliderChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureSliderChangeStatusToFalse(string id) {
            //await _featureSliderService.FeatureSliderChageStatusToFalse(id);

            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("https://localhost:7070/api/FeatureSliders/FeatureSliderChangeStatusToFalse/" + id);
            if (res.IsSuccessStatusCode) {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        private void FeatureSliderViewBaglist(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slider Öne Çıkan Görsel İşlemleri";
            ViewBag.v4 = v4;
        }
    }
}
