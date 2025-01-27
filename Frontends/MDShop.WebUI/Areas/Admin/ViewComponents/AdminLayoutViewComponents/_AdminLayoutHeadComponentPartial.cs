using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents {
    public class _AdminLayoutHeadComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
