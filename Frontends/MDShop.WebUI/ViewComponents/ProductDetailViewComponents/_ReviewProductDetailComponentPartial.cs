using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _ReviewProductDetailComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
