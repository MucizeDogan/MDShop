using StackExchange.Redis;

namespace MDShop.Basket.Settings {
    public class RedisService {
        //Redis için configurasyon. 2 tane parametremiz var localhost port. bu ikisini property olarak tanımlayıp const geçtik.
        public string _host { get; set; }
        public int _port { get; set; }

        private ConnectionMultiplexer _connectionMultiplexer; // Redis'e bağlantı işlemi gerçekleştirebilmek için köprü görevi görecek.
        public RedisService(string host, int port) {
            _host = host;
            _port = port;
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}"); //Connect metodu çağrıldı zaman host ve porta göre gidecek
        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(0); //Redis ilk oluştuğunda 16 tane database oluşturuyor biz bunlardan ilkini kullanacağız diyoruz burada
    }
}
