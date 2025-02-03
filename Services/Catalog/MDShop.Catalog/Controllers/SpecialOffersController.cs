using MDShop.Catalog.Dtos.SpecialOfferDtos;
using MDShop.Catalog.Services.SpecialOfferServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.Catalog.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase {
        private readonly ISpecialOfferService _specialOfferService;
        public SpecialOffersController(ISpecialOfferService SpecialOfferService) {
            _specialOfferService = SpecialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> SpecialOfferList(bool isAdmin) {
            var values = await _specialOfferService.GetAllSpecialOfferAsync(isAdmin);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id) {
            var values = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto) {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Özel teklif başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string id) {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Özel teklif başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto) {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Özel teklif başarıyla güncellendi");
        }

        [HttpGet("SpecialOfferChangeStatusToTrue/{id}")]
        public async Task<IActionResult> SpecialOfferChangeStatusToTrue(string id) {
            await _specialOfferService.SpecialOfferChangeStatusToTrue(id);
            return Ok("Özel teklif durumu başarıyla TRUE oldu");
        }

        [HttpGet("SpecialOfferChangeStatusToFalse/{id}")]
        public async Task<IActionResult> SpecialOfferChangeStatusToFalse(string id) {
            await _specialOfferService.SpecialOfferChangeStatusToFalse(id);
            return Ok("Özel teklif durumu başarıyla FALSE oldu");
        }
    }
}
