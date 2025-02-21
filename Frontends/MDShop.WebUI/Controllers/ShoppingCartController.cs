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
            var values = await _basketService.GetBasket();
            return View(values);
        }

        public async Task<IActionResult> AddBasketItem(string productId) {
            var values = await _productService.GetByIdProductAsync(productId);
            var items = new BasketItemDto {
                ProductId = values.ProductID,
                ProductName = values.ProductName,
                Price = values.ProductPrice,
                Quantity = 1
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
