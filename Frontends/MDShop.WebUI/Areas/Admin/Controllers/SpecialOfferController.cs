using MDShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MDShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/SpecialOffer")]
    public class SpecialOfferController : Controller {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOfferController(ISpecialOfferService specialOfferService) {
            _specialOfferService = specialOfferService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            SpecialOfferViewbagList("Özel Teklif Listesi");

            var values = await _specialOfferService.GetAllSpecialOfferAsync(true);
            return View(values);
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
            try {
                await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            } catch (Exception ex) {
                return View(createSpecialOfferDto);
            }
        }

        [Route("DeleteSpecialOffer/{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id) {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });

        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id) {
            SpecialOfferViewbagList("Özel Teklif Güncelleme");
            var value = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return View(value);
            
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto) {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        [Route("SpecialOfferChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> SpecialOfferChangeStatusToTrue(string id) {
            await _specialOfferService.SpecialOfferChangeStatusToTrue(id);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        [Route("SpecialOfferChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> SpecialOfferChangeStatusToFalse(string id) {
            await _specialOfferService.SpecialOfferChangeStatusToFalse(id);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        void SpecialOfferViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
