using MDShop.Catalog.Dtos.FeatureSliderDtos;
using MDShop.Catalog.Services.FeatureSliderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.Catalog.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSlidersController : ControllerBase {
        private readonly IFeatureSliderService _FeatureSliderService;
        public FeatureSlidersController(IFeatureSliderService FeatureSliderService) {
            _FeatureSliderService = FeatureSliderService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureSliderList() {
            var values = await _FeatureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureSliderById(string id) {
            var values = await _FeatureSliderService.GetByIdFeatureSliderAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto) {
            await _FeatureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return Ok("Öne çıkan görsel başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeatureSlider(string id) {
            await _FeatureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Öne çıkan görsel başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto) {
            await _FeatureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Öne çıkan görsel başarıyla güncellendi");
        }

        [HttpGet("FeatureSliderChangeStatusToTrue/{id}")]
        public async Task<IActionResult> FeatureSliderChangeStatusToTrue(string id) {
            await _FeatureSliderService.FeatureSliderChangeStatusToTrue(id);
            return Ok("Öne çıkan görselin durumu başarıyla TRUE oldu");
        }

        [HttpGet("FeatureSliderChangeStatusToFalse/{id}")]
        public async Task<IActionResult> FeatureSliderChangeStatusToFalse(string id) {
            await _FeatureSliderService.FeatureSliderChangeStatusToFalse(id);
            return Ok("Öne çıkan görselin durumu başarıyla FALSE oldu");
        }
    }
}
