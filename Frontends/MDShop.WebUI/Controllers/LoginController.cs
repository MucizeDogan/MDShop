using MDShop.DtoLayer.IdentityDtos.LoginDtos;
using MDShop.WebUI.Models;
using MDShop.WebUI.Services.LoginServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MDShop.WebUI.Controllers {
    public class LoginController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IIdentityService _identityService;

        public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService, IIdentityService identityService) {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto) {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8,"application/json");
            var res = await client.PostAsync("http://localhost:5001/api/Logins", content);
            if (res.IsSuccessStatusCode) {
                var jsonData = await res.Content.ReadAsStringAsync();
                // Buraya kadar klasik bildiğimiz şeyler. Bundan sonraki olay sisteme giriş yapan kullanıcı için bir token ürettireceğiz. Bu token ın doğruluğu kontrol edilecek.
                var tokenModel = JsonSerializer.Deserialize<JWTResponseModel>(jsonData, new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                if (tokenModel != null) {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null) {
                        claims.Add(new Claim("mdsshoptoken",tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        var id = _loginService.GetUserId; // giriş yapan kullanıcının id değerini alıyoruz
                        return RedirectToAction("Index", "Default");
                    }
                }
            }
            return View();
        }

        //[HttpGet]
        //public IActionResult SignIn() {
        //    return View();
        //}

        //[HttpPost]
        public async Task<IActionResult> SignIn(SignInDto signInDto) {
            signInDto.Username = "cristdoan";
            signInDto.Password = "123456aA*";
            await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "Test");
        }
    }
}
