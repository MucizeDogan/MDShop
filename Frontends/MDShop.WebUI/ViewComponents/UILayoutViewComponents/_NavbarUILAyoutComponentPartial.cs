using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _NavbarUILAyoutComponentPartial : ViewComponent{
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
