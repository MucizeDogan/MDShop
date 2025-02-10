using AutoMapper;
using MDShop.Catalog.Dtos.ProductDtos;
using MDShop.Catalog.Dtos.ProductImageDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.ProductImageServices {
    public class ProductImageService : IProductImageService {
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı kuruyoruz connectionString ile
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Kurulan Bağlantıyla veritabanına ulaşıyoruz
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto) {
            var value = _mapper.Map<ProductImage>(createProductImageDto);
            await _productImageCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductImageAsync(string id) {
            await _productImageCollection.DeleteOneAsync(x => x.ProductImageID == id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync() {
            var values = await _productImageCollection.Find(x => true).ToListAsync(); // Bütün değerleri bul ve listele
            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id) {
            var value = await _productImageCollection.Find(x => x.ProductImageID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(value);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto) {
            var value = _mapper.Map<ProductImage>(updateProductImageDto);
            await _productImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, value);
        }

        public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id) {
            var values = await _productImageCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }
    }
}
