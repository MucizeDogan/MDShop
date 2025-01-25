using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _TopbarUILayoutComponentPartial : ViewComponent {

        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
