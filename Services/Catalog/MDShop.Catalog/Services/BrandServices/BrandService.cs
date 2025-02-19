using AutoMapper;
using MDShop.Catalog.Dtos.BrandDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.BrandServices {
    public class BrandService : IBrandService {
        private readonly IMongoCollection<Brand> _brandCollection;
        private readonly IMapper _mapper;
        public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync(bool isAdmin) {
            if (isAdmin) {
                var values = await _brandCollection.Find(x => true).ToListAsync();
                return _mapper.Map<List<ResultBrandDto>>(values);
            }
            var values2 = await _brandCollection
                .Find(x => x.Status == true)  // Status true olanları getir
                .SortBy(x => x.Order)         // Order alanına göre sırala
                .ToListAsync();
            return _mapper.Map<List<ResultBrandDto>>(values2);
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto) {

            var exists = await _brandCollection
                .Find(x => x.Order == createBrandDto.Order)
                .AnyAsync(); // daha önce bu order a (sıraya) sahip bir kayıt var mı

            if (!exists) {
                var value = _mapper.Map<Brand>(createBrandDto);
                await _brandCollection.InsertOneAsync(value);
            } else {
                throw new ApplicationException("Bu sıra numarasına sahip bir kayıt zaten mevcut.");
            }
        }

        public async Task DeleteBrandAsync(string id) {
            await _brandCollection.DeleteOneAsync(x => x.BrandId == id);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto) {
            var values = _mapper.Map<Brand>(updateBrandDto);
            await _brandCollection.FindOneAndReplaceAsync(x => x.BrandId == updateBrandDto.BrandId, values);
        }

        public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id) {
            var values = await _brandCollection.Find<Brand>(x => x.BrandId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdBrandDto>(values);
        }

        public async Task BrandChangeStatusToTrue(string id) {
            var value = await _brandCollection.Find<Brand>(x => x.BrandId == id).FirstOrDefaultAsync();
            value.Status = true;
            await _brandCollection.ReplaceOneAsync(x => x.BrandId == id, value);
        }

        public async Task BrandChangeStatusToFalse(string id) {
            var value = await _brandCollection.Find<Brand>(x => x.BrandId == id).FirstOrDefaultAsync();
            value.Status = false;
            await _brandCollection.ReplaceOneAsync(x => x.BrandId == id, value);
        }
    }
}
