using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ShoppingCartViewComponents {
    public class _ShoppingCarDiscountCouponComponentPartial  : ViewComponent{
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
