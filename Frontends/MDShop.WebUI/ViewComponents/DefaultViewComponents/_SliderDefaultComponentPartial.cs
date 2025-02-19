using MDShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MDShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _SliderDefaultComponentPartial : ViewComponent{
        private readonly IFeatureSliderService _featureSliderService;

        public _SliderDefaultComponentPartial(IFeatureSliderService featureSliderService) {
            _featureSliderService = featureSliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var values = await _featureSliderService.GetAllFeatureSliderAsync(false);
            return View(values);
        }
    }
}
