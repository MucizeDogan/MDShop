using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.ProductDetailViewComponents {
    public class _DescriptionProductDetailComponentPartial : ViewComponent{
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
