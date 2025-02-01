using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MDShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/FeatureSlider")]
    public class FeatureSliderController : Controller {
        private readonly IFeatureSliderService _featureSliderService;
        public FeatureSliderController(IFeatureSliderService featureSliderService) {
            _featureSliderService = featureSliderService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Listesi");
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
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
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id) {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id) {
            FeatureSliderViewBaglist("Slider (Öne Çıkan Görsel) Güncelleme");
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return View(values);
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto) {

            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        [Route("FeatureSliderChageStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureSliderChageStatusToTrue(string id) {
            await _featureSliderService.FeatureSliderChageStatusToTrue(id);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        [Route("FeatureSliderChageStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureSliderChageStatusToFalse(string id) {
            await _featureSliderService.FeatureSliderChageStatusToFalse(id);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        private void FeatureSliderViewBaglist(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slider Öne Çıkan Görsel İşlemleri";
            ViewBag.v4 = v4;
        }
    }
}
