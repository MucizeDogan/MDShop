﻿using System.Security.Claims;

namespace MDShop.WebUI.Services.LoginServices {
    public class LoginService : ILoginService {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; // Kullanıcı bilgilerini verecek
    }
}
