using MDShop.DtoLayer.BasketDtos;
using MDShop.WebUI.Services.BasketServices;
using MDShop.WebUI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Controllers {
    public class ShoppingCartController : Controller {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public ShoppingCartController(IProductService productService, IBasketService basketService) {
            _productService = productService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index() {
            ViewBag.path1 = "Ana Sayfa";
            ViewBag.path3 = "Sepetim";

            var basketTotal = await _basketService.GetBasket();
            ViewBag.totalPrice = basketTotal.TotalPrice.ToString("#,##0.00");

            var TaxRate = 10; // %10
            var TaxPrice = (basketTotal.TotalPrice * TaxRate) / 100;
            var totalPriceWithTax = basketTotal.TotalPrice + TaxPrice;
            ViewBag.TaxRate = TaxRate;
            ViewBag.TaxPrice = TaxPrice.ToString("#,##0.00");
            ViewBag.totalPriceWithTax = totalPriceWithTax.ToString("#,##0.00");

            return View();
        }

        public async Task<IActionResult> AddBasketItem(string id) {
            var values = await _productService.GetByIdProductAsync(id);
            var items = new BasketItemDto {
                ProductId = values.ProductID,
                ProductName = values.ProductName,
                Price = values.ProductPrice,
                Quantity = 1,
                ProductImageUrl = values.ProductImageUrl
            };
            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveBasketItem(string id) {
            await _basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }
    }
}
