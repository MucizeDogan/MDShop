using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class DefaultController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
