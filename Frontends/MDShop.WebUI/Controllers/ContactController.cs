using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class ContactController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
