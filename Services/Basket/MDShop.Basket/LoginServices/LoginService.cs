namespace MDShop.Basket.LoginServices {
    public class LoginService : ILoginService {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor) {
            _httpContextAccessor = contextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;   //sub ını yakalayacağım neden? sub ın içerisinde id var sub tokendan gelecek.
    }
}
