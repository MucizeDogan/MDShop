using MDShop.WebUI.Models;
using MDShop.WebUI.Services.Interfaces;

namespace MDShop.WebUI.Services.Concrete {
    public class UserServie : IUserService {
        private readonly HttpClient _httpClient;

        public UserServie(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserInfo() {
            return await _httpClient.GetFromJsonAsync<UserDetailViewModel>("/api/users/getuserinfo");
        }
    }
}
