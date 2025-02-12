using MDShop.Catalog.Dtos.FeatureDtos;
using MDShop.Catalog.Services.FeatureServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.Catalog.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase {
        private readonly IFeatureService _featureService;
        public FeaturesController(IFeatureService FeatureService) {
            _featureService = FeatureService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureList(bool isAdmin) {
            var values = await _featureService.GetAllFeatureAsync(isAdmin);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(string id) {
            var values = await _featureService.GetByIdFeatureAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto) {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return Ok("Öne Çıkan Alan başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(string id) {
            await _featureService.DeleteFeatureAsync(id);
            return Ok("Öne Çıkan Alan başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto) {
            await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return Ok("Öne Çıkan Alan başarıyla güncellendi");
        }

        [HttpGet("FeatureChangeStatusToTrue/{id}")]
        public async Task<IActionResult> FeatureChangeStatusToTrue(string id) {
            await _featureService.FeatureChangeStatusToTrue(id);
            return Ok("Öne Çıkan Alan durumu başarıyla TRUE oldu");
        }

        [HttpGet("FeatureChangeStatusToFalse/{id}")]
        public async Task<IActionResult> FeatureChangeStatusToFalse(string id) {
            await _featureService.FeatureChangeStatusToFalse(id);
            return Ok("Öne Çıkan Alan durumu başarıyla FALSE oldu");
        }
    }
}
