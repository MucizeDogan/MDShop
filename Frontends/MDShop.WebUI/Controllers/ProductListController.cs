using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class ProductListController : Controller {
        public IActionResult Index(string id) {
            ViewBag.i = id; // id değerini taşımak için
            return View();
        }

        public IActionResult ProductDetail() {
            return View();
        }
    }
}
