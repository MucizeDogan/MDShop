using AutoMapper;
using MDShop.Catalog.Dtos.CategoryDtos;
using MDShop.Catalog.Dtos.ProductDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.ProductServices {
    public class ProductService : IProductService {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı kuruyoruz connectionString ile
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Kurulan Bağlantıyla veritabanına ulaşıyoruz
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto) {
            var value = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id) {
            await _productCollection.DeleteOneAsync(x => x.ProductID == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync() {
            var values = await _productCollection.Find(x => true).ToListAsync(); // Bütün değerleri bul ve listele
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id) {
            var value = await _productCollection.Find(x=>x.ProductID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync() {
            //var values = await _productCollection.Find(x => true).ToListAsync();
            //foreach (var item in values) {
            //    item.Category = await _categoryCollection.Find(x => x.CategoryID == item.CategoryID).FirstAsync();
            //}
            //return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);

            var products = await _productCollection.Find(x => true).ToListAsync();
            var categoryIds = products.Select(x => x.CategoryID).Distinct().ToList();

            var categories = await _categoryCollection.Find(x => categoryIds.Contains(x.CategoryID)).ToListAsync();

            foreach (var product in products) {
                product.Category = categories.FirstOrDefault(x => x.CategoryID == product.CategoryID);
            }

            return _mapper.Map<List<ResultProductsWithCategoryDto>>(products);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto) {
            var value = _mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID, value);
        }
    }
}
