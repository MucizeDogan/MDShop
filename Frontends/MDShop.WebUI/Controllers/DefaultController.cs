using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class DefaultController : Controller {
        public IActionResult Index() {
            ViewBag.path1 = "Ana Sayfa";
            ViewBag.path3 = "Ürün Listesi";
            return View();
        }
    }
}
