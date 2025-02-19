using MDShop.DtoLayer.CatalogDtos.FeatureDtos;
using MDShop.WebUI.Services.CatalogServices.FeatureServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class FeatureController : Controller {
        private readonly IFeatureService _FeatureService;

        public FeatureController(IFeatureService FeatureService) {
            _FeatureService = FeatureService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            FeatureViewbagList("Öne Çıkan Alan Listesi");
            var values = await _FeatureService.GetAllFeatureAsync(true);
            return View(values);
        }

        [HttpGet]
        [Route("CreateFeature")]
        public IActionResult CreateFeature() {
            FeatureViewbagList("Öne Çıkan Alan Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateFeature")]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto) {
            await _FeatureService.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id) {
            await _FeatureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Route("UpdateFeature/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id) {
            FeatureViewbagList("Öne Çıkan Alan Güncelleme");
            var values = await _FeatureService.GetByIdFeatureAsync(id);
            return View(values);
        }

        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto) {

            await _FeatureService.UpdateFeatureAsync(updateFeatureDto);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Route("FeatureChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureChangeStatusToTrue(string id) {
            await _FeatureService.FeatureChangeStatusToTrue(id);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Route("FeatureChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> FeatureChangeStatusToFalse(string id) {
            await _FeatureService.FeatureChangeStatusToFalse(id);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        void FeatureViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan Alanlar";
            ViewBag.v3 = "Öne Çıkan Alan İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
