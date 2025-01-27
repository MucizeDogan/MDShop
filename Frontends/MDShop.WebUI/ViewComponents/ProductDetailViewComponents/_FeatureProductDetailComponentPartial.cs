using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _FeatureProductDetailComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
