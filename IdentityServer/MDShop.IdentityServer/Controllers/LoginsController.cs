using MDShop.IdentityServer.Dtos;
using MDShop.IdentityServer.Models;
using MDShop.IdentityServer.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MDShop.IdentityServer.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase {
        private readonly SignInManager<ApplicationUser> _signInManager; // Identity de giriş yapmak için kullanılan bir sınıf
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto) {
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (result.Succeeded) {
                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                model.Username = userLoginDto.Username;
                model.Id = user.Id;
                var token = JwtTokenGenerator.GenerateToken(model);
                return Ok(token);
                
            } else {
                return Ok("Kullanıcı Adı veya Şifre Hatalı");
            }
        }
    }
}
