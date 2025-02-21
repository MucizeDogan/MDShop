using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ShoppingCartViewComponents {
    public class _ShoppingCartProductListComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
