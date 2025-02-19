using MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MDShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _OfferDiscountDefaultComponentPartial : ViewComponent{
        private readonly IOfferDiscountService _offerDiscountService;

        public _OfferDiscountDefaultComponentPartial(IOfferDiscountService offerDiscountService) {
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var values = await _offerDiscountService.GetAllOfferDiscountAsync(false);
            return View(values);
        }
    }
}
