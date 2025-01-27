using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Areas.Admin.Controllers {
    public class CategoryController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
