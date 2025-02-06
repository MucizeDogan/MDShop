using MDShop.Catalog.Dtos.OfferDiscountDtos;
using MDShop.Catalog.Services.OfferDiscountServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.Catalog.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OfferDiscountsController : ControllerBase {
        private readonly IOfferDiscountService _OfferDiscountService;
        public OfferDiscountsController(IOfferDiscountService OfferDiscountService) {
            _OfferDiscountService = OfferDiscountService;
        }

        [HttpGet]
        public async Task<IActionResult> OfferDiscountList(bool isAdmin) {
            var values = await _OfferDiscountService.GetAllOfferDiscountAsync(isAdmin);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferDiscountById(string id) {
            var values = await _OfferDiscountService.GetByIdOfferDiscountAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto) {
            await _OfferDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
            return Ok("Özel İndirim Teklifi başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOfferDiscount(string id) {
            await _OfferDiscountService.DeleteOfferDiscountAsync(id);
            return Ok("Özel İndirim Teklifi başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto) {
            await _OfferDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
            return Ok("Özel İndirim Teklifi başarıyla güncellendi");
        }

        [HttpGet("OfferDiscountChangeStatusToTrue/{id}")]
        public async Task<IActionResult> OfferDiscountChangeStatusToTrue(string id) {
            await _OfferDiscountService.OfferDiscountChangeStatusToTrue(id);
            return Ok("Özel İndirim Teklifi durumu başarıyla TRUE oldu");
        }

        [HttpGet("OfferDiscountChangeStatusToFalse/{id}")]
        public async Task<IActionResult> OfferDiscountChangeStatusToFalse(string id) {
            await _OfferDiscountService.OfferDiscountChangeStatusToFalse(id);
            return Ok("Özel İndirim Teklifi durumu başarıyla FALSE oldu");
        }
    }
}
