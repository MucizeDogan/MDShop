﻿using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.ViewComponents.DefaultViewComponents {
    public class _SliderDefaultComponentPartial : ViewComponent{
        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
