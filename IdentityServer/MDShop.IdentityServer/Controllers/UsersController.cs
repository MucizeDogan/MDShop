using MDShop.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MDShop.IdentityServer.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo() {
            var userClaim = User.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Sub); // Tokendaki id değeri
            var user = await _userManager.FindByIdAsync(userClaim.Value);
            return Ok(new {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                UserName = user.UserName
            });
        }
    }
}
