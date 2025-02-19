using MDShop.DtoLayer.CatalogDtos.FeatureDtos;
using MDShop.WebUI.Services.CatalogServices.FeatureServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _FeatureDefaultComponentPartial : ViewComponent {
        private readonly IFeatureService _featureService;

        public _FeatureDefaultComponentPartial(IFeatureService featureService) {
            _featureService = featureService;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var values = await _featureService.GetAllFeatureAsync(false);
            return View(values);
        }
    }
}
