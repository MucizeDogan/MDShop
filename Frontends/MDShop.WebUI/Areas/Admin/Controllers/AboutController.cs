using MDShop.DtoLayer.CatalogDtos.AboutDtos;
using MDShop.WebUI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/About")]
    public class AboutController : Controller {
        private readonly IAboutService _AboutService;
        public AboutController(IAboutService AboutService) {
            _AboutService = AboutService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            AboutViewbagList("Hakkımızda Listesi");
            var values = await _AboutService.GetAllAboutAsync();
            return View(values);
        }

        [HttpGet]
        [Route("CreateAbout")]
        public IActionResult CreateAbout() {
            AboutViewbagList("Hakkımızda Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto) {
            await _AboutService.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id) {
            await _AboutService.DeleteAboutAsync(id);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        [Route("UpdateAbout/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id) {
            AboutViewbagList("Hakkımızda Güncelleme");
            var values = await _AboutService.GetByIdAboutAsync(id);
            return View(values);
        }
        [Route("UpdateAbout/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto) {
            await _AboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        void AboutViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
