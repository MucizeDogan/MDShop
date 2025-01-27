using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductListViewComponents {
    public class _PriceFilterProductListComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
