using AutoMapper;
using MDShop.Catalog.Dtos.CategoryDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.CategoryServices {
    public class CategoryService : ICategoryService {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı kuruyoruz connectionString ile
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Kurulan Bağlantıyla veritabanına ulaşıyoruz
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); // veritabanı üzerinden de tabloya ulaşacağız. Burada _categoryCollection a atama yaparak tabloya ulaşıyoruz
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto) {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id) {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync() {
            var values = await _categoryCollection.Find(x=>true).ToListAsync(); // Bütün değerleri bul ve listele
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id) {
            var values = await _categoryCollection.Find(x => x.CategoryID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDto>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto) {
            var value = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryID == updateCategoryDto.CategoryID, value);
        }
    }
}
