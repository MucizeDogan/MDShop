using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _InformationProductDetailComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
