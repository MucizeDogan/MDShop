using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductListViewComponents {
    public class _ColorFilterProductListComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
