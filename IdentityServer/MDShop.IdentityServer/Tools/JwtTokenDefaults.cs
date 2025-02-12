namespace MDShop.IdentityServer.Tools {
    public class JwtTokenDefaults {
        public const string ValidAudience = "http://localhost"; // Tokenı kimin yayınladığını belirtir.
        public const string ValidIssuer = "http://localhost"; //Dinleyici
        public const string Key = "MDShop.0307.MucizeDoganSarikurkcu./*-+";
        public const int Expire = 60; // Tokenın geçerlilik süresi
    }
}
