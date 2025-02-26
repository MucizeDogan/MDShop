using MDShop.WebUI.Services.BasketServices;
using MDShop.WebUI.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class DiscountController : Controller {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService) {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon() {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code) {
            var values = await _discountService.GetDiscountCode(code);
            
            var basketTotal = await _basketService.GetBasket();
            ViewBag.totalPrice = basketTotal.TotalPrice.ToString("#,##0.00");

            var TaxRate = 10; // %10
            var TaxPrice = (basketTotal.TotalPrice * TaxRate) / 100;
            var totalPriceWithTax = basketTotal.TotalPrice + TaxPrice;
            ViewBag.TaxRate = TaxRate;
            ViewBag.TaxPrice = TaxPrice.ToString("#,##0.00");
            ViewBag.totalPriceWithTax = totalPriceWithTax.ToString("#,##0.00");

            //var discountRate = values.Rate;
            //if (values.Rate > 0) {
            //    var discountPrice = totalPriceWithTax / 100 * discountRate;
            //    var discountedTotalPriceWithTax = totalPriceWithTax - discountPrice;
            //    ViewBag.discountedPrice = discountedTotalPriceWithTax.ToString("#,##0.00");
            //}

            //return View(values);

            return RedirectToAction("Index","ShoppingCart", new {code = code});
        }
    }
}
