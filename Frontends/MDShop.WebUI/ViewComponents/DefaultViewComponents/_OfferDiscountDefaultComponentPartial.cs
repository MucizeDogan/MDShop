using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _OfferDiscountDefaultComponentPartial : ViewComponent{
        public IViewComponentResult Invoke() {
            return View();  
        }
    }
}
