using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents {
    public class _AdminLayoutSideBarComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
