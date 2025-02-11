using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class LoginController : Controller {
        [HttpGet]
        public IActionResult Index() {
            return View();
        }
    }
}
