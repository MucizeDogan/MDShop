using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _CategoriesDefaultComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
