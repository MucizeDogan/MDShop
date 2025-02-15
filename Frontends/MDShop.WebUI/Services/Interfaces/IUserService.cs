using MDShop.WebUI.Models;

namespace MDShop.WebUI.Services.Interfaces {
    public interface IUserService {
        Task<UserDetailViewModel> GetUserInfo();
    }
}
