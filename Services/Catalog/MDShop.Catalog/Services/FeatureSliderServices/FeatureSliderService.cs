using AutoMapper;
using MDShop.Catalog.Dtos.FeatureSliderDtos;
using MDShop.Catalog.Dtos.SpecialOfferDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.FeatureSliderServices {
    public class FeatureSliderService : IFeatureSliderService {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;
        public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);
            _mapper = mapper;
        }
        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto) {
            //var value = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
            //await _featureSliderCollection.InsertOneAsync(value);

            var exists = await _featureSliderCollection
                .Find(x => x.Order == createFeatureSliderDto.Order)
                .AnyAsync(); // daha önce bu order a (sıraya) sahip bir kayıt var mı

            if (!exists) {
                var value = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
                await _featureSliderCollection.InsertOneAsync(value);
            } else {
                throw new Exception("Bu sıra numarasına sahip bir kayıt zaten mevcut.");
            }
        }

        public async Task DeleteFeatureSliderAsync(string id) {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == id);
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id) {
            var values = await _featureSliderCollection.Find<FeatureSlider>(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureSliderDto>(values);
        }

        public async Task FeatureSliderChangeStatusToFalse(string id) {
            var value = await _featureSliderCollection.Find<FeatureSlider>(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            value.Status = false;
            await _featureSliderCollection.ReplaceOneAsync(x => x.FeatureSliderId == id, value);
        }

        public async Task FeatureSliderChangeStatusToTrue(string id) {
            var value = await _featureSliderCollection.Find<FeatureSlider>(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            value.Status = true;
            await _featureSliderCollection.ReplaceOneAsync(x => x.FeatureSliderId == id, value);
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync(bool isAdmin) {
            ////var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            //var values = await _featureSliderCollection
            //    .Find(x => x.Status == true)  // Status true olanları getir
            //    .SortBy(x => x.Order)         // Order alanına göre sırala
            //    .ToListAsync();
            //return _mapper.Map<List<ResultFeatureSliderDto>>(values);

            if (isAdmin) {
                var values = await _featureSliderCollection.Find(x => true).ToListAsync();
                return _mapper.Map<List<ResultFeatureSliderDto>>(values);
            }
            var values2 = await _featureSliderCollection
                .Find(x => x.Status == true)  // Status true olanları getir
                .SortBy(x => x.Order)         // Order alanına göre sırala
                .ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(values2);
        }


        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto) {
            var values = _mapper.Map<FeatureSlider>(updateFeatureSliderDto);
            await _featureSliderCollection.FindOneAndReplaceAsync(x => x.FeatureSliderId == updateFeatureSliderDto.FeatureSliderId, values);
        }
    }
}
