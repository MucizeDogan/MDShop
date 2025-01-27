using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductListViewComponents {
    public class _PaginationProductListComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
