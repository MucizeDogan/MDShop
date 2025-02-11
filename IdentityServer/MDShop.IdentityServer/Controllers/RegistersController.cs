﻿using MDShop.IdentityServer.Dtos;
using MDShop.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MDShop.IdentityServer.Controllers {
    //[Authorize(LocalApi.PolicyName)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto) {
            var values = new ApplicationUser() { 
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
            };
            var result = await _userManager.CreateAsync(values, userRegisterDto.Password);
            if (result.Succeeded) {
                return Ok("Kullanıcı başarıyla eklendi");
            } else {
                return Ok("Bir hata oluştu tekrar deneyiniz");
            }
        }
    }
}
