using MDShop.DtoLayer.IdentityDtos.LoginDtos;

namespace MDShop.WebUI.Services.LoginServices {
    public interface IIdentityService {
        Task<bool> SignIn(SignInDto signInDto);
        Task<bool> GetRefreshToken();
    }
}
