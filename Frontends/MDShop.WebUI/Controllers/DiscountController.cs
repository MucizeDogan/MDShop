using MDShop.WebUI.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class DiscountController : Controller {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService) {
            _discountService = discountService;
        }

        [HttpPost]
        public IActionResult ConfirmDiscountCoupon(string code) {
            code = "MDS20";
            var values = _discountService.GetDiscountCode(code);
            return View(values);
        }
    }
}
