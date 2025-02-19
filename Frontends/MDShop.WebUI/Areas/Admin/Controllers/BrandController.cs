using MDShop.DtoLayer.CatalogDtos.BrandDtos;
using MDShop.WebUI.Services.CatalogServices.BrandServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller {
        private readonly IBrandService _BrandService;

        public BrandController(IBrandService BrandService) {
            _BrandService = BrandService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            BrandViewbagList("Markalar Listesi");
            var values = await _BrandService.GetAllBrandAsync(true);
            return View(values);
        }

        [HttpGet]
        [Route("CreateBrand")]
        public IActionResult CreateBrand() {
            BrandViewbagList("Markalar Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto) {
            try {
                await _BrandService.CreateBrandAsync(createBrandDto);
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            } catch (Exception ex) {
                // Hata mesajını TempData ile taşıyoruz
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Brand", new { area = "Admin" });
            }
        }

        [Route("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(string id) {
            await _BrandService.DeleteBrandAsync(id);
            return RedirectToAction("Index", "Brand", new { area = "Admin" });
        }

        [Route("UpdateBrand/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id) {
            BrandViewbagList("Markalar Güncelleme");
            var values = await _BrandService.GetByIdBrandAsync(id);
            return View(values);
        }

        [Route("UpdateBrand/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto) {

            await _BrandService.UpdateBrandAsync(updateBrandDto);
            return RedirectToAction("Index", "Brand", new { area = "Admin" });
        }

        [Route("BrandChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> BrandChangeStatusToTrue(string id) {
            await _BrandService.BrandChangeStatusToTrue(id);
            return RedirectToAction("Index", "Brand", new { area = "Admin" });
        }

        [Route("BrandChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> BrandChangeStatusToFalse(string id) {
            await _BrandService.BrandChangeStatusToFalse(id);
            return RedirectToAction("Index", "Brand", new { area = "Admin" });
        }

        void BrandViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Markalar";
            ViewBag.v3 = "Marka İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
