namespace MDShop.WebUI.Settings {
    public class ClientSettings {
        public Client MDShopVisitorClient { get; set; }
        public Client MDShopManagerClient { get; set; }
        public Client MDShopAdminClient { get; set; }

        public class Client {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }
    }
}
