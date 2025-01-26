using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _VendorDefaultComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
