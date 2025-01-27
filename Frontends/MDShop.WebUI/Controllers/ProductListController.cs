using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class ProductListController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
