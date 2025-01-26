using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _FooterUILayoutComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
