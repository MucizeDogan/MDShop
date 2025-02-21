using MDShop.DtoLayer.IdentityDtos.LoginDtos;
using MDShop.WebUI.Models;
using MDShop.WebUI.Services.Interfaces;
using MDShop.WebUI.Services.LoginServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MDShop.WebUI.Controllers
{
    public class LoginController : Controller {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;

        public LoginController(IHttpClientFactory httpClientFactory, IIdentityService identityService) {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto) {

            var res = await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "User");
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
            return RedirectToAction("Index", "User");
        }
    }
}
