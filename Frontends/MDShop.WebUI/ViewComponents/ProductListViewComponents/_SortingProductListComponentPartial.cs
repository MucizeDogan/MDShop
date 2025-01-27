using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductListViewComponents {
    public class _SortingProductListComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
