using MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MDShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("Admin/OfferDiscount")]
    public class OfferDiscountController : Controller {
        private readonly IOfferDiscountService _OfferDiscountService;

        public OfferDiscountController(IOfferDiscountService OfferDiscountService) {
            _OfferDiscountService = OfferDiscountService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index() {
            OfferDiscountViewbagList("İndirim Teklifleri Listesi");
            var values = await _OfferDiscountService.GetAllOfferDiscountAsync(true);
            return View(values);
        }

        [HttpGet]
        [Route("CreateOfferDiscount")]
        public IActionResult CreateOfferDiscount() {
            OfferDiscountViewbagList("İndirim Teklifleri Ekleme");
            return View();
        }

        [HttpPost]
        [Route("CreateOfferDiscount")]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto) {
            await _OfferDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        [Route("DeleteOfferDiscount/{id}")]
        public async Task<IActionResult> DeleteOfferDiscount(string id) {
            await _OfferDiscountService.DeleteOfferDiscountAsync(id);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        [Route("UpdateOfferDiscount/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateOfferDiscount(string id) {
            OfferDiscountViewbagList("İndirim Teklifleri Güncelleme");
            var values = await _OfferDiscountService.GetByIdOfferDiscountAsync(id);
            return View(values);
        }

        [Route("UpdateOfferDiscount/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto) {

            await _OfferDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        [Route("OfferDiscountChangeStatusToTrue/{id}")]
        [HttpGet]
        public async Task<IActionResult> OfferDiscountChangeStatusToTrue(string id) {
            await _OfferDiscountService.OfferDiscountChangeStatusToTrue(id);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        [Route("OfferDiscountChangeStatusToFalse/{id}")]
        [HttpGet]
        public async Task<IActionResult> OfferDiscountChangeStatusToFalse(string id) {
            await _OfferDiscountService.OfferDiscountChangeStatusToFalse(id);
            return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" });
        }

        void OfferDiscountViewbagList(string v4) {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirim Teklifiler";
            ViewBag.v3 = "İndirim Teklifi İşlemleri";
            ViewBag.v4 = v4;

        }
    }
}
