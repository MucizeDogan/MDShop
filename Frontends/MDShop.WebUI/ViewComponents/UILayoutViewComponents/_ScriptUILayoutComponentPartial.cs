using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.UILayoutViewComponents {
    public class _ScriptUILayoutComponentPartial : ViewComponent {
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
