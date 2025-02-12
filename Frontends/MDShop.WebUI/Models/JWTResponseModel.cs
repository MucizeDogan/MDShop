namespace MDShop.WebUI.Models {
    public class JWTResponseModel {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; } // Son Geçerlilik Tarihi
    }
}
