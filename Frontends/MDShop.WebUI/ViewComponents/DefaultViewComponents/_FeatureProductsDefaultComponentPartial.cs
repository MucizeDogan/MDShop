using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _FeatureProductsDefaultComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
