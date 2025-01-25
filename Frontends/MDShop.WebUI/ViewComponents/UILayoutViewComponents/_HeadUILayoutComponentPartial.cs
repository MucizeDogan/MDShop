using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _HeadUILayoutComponentPartial : ViewComponent {

        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
