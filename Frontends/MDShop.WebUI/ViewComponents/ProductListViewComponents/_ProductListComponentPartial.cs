using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductListViewComponents {
    public class _ProductListComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
