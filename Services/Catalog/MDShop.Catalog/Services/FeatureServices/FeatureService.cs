using AutoMapper;
using MDShop.Catalog.Dtos.FeatureDtos;
using MDShop.Catalog.Dtos.SpecialOfferDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.FeatureServices {
    public class FeatureService : IFeatureService {
        private readonly IMongoCollection<Feature> _featureCollection;
        private readonly IMapper _mapper;
        public FeatureService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync(bool isAdmin) {
            if (isAdmin) {
                var values = await _featureCollection.Find(x => true).ToListAsync();
                return _mapper.Map<List<ResultFeatureDto>>(values);
            }
            var values2 = await _featureCollection
                .Find(x => x.Status == true)  // Status true olanları getir
                .SortBy(x => x.Order)         // Order alanına göre sırala
                .ToListAsync();
            return _mapper.Map<List<ResultFeatureDto>>(values2);
        }

        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto) {
            var exists = await _featureCollection
                .Find(x => x.Order == createFeatureDto.Order)
                .AnyAsync(); // daha önce bu order a (sıraya) sahip bir kayıt var mı

            if (!exists) {
                var value = _mapper.Map<Feature>(createFeatureDto);
                await _featureCollection.InsertOneAsync(value);
            } else {
                throw new Exception("Bu sıra numarasına sahip bir kayıt zaten mevcut.");
            }
        }

        public async Task DeleteFeatureAsync(string id) {
            await _featureCollection.DeleteOneAsync(x => x.FeatureId == id);
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id) {
            var values = await _featureCollection.Find<Feature>(x => x.FeatureId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureDto>(values);
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto) {
            var values = _mapper.Map<Feature>(updateFeatureDto);
            await _featureCollection.FindOneAndReplaceAsync(x => x.FeatureId == updateFeatureDto.FeatureId, values);
        }

        public async Task FeatureChangeStatusToFalse(string id) {
            var value = await _featureCollection.Find<Feature>(x => x.FeatureId == id).FirstOrDefaultAsync();
            value.Status = false;
            await _featureCollection.ReplaceOneAsync(x => x.FeatureId == id, value);
        }

        public async Task FeatureChangeStatusToTrue(string id) {
            var value = await _featureCollection.Find<Feature>(x => x.FeatureId == id).FirstOrDefaultAsync();
            value.Status = true;
            await _featureCollection.ReplaceOneAsync(x => x.FeatureId == id, value);
        }

    }
}
