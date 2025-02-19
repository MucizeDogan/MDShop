using MDShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MDShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _SpecialOfferComponentPartial : ViewComponent {
        private readonly ISpecialOfferService _specialOfferService;

        public _SpecialOfferComponentPartial(ISpecialOfferService specialOfferService) {
            _specialOfferService = specialOfferService;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var values = await _specialOfferService.GetAllSpecialOfferAsync(false);
            return View(values);
        }
    }
}
