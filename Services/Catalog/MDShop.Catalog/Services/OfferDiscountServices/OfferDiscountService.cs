using AutoMapper;
using MDShop.Catalog.Dtos.OfferDiscountDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.OfferDiscountServices {
    public class OfferDiscountService : IOfferDiscountService {
        private readonly IMongoCollection<OfferDiscount> _offerDiscountCollection;
        private readonly IMapper _mapper;
        public OfferDiscountService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _offerDiscountCollection = database.GetCollection<OfferDiscount>(_databaseSettings.OfferDiscountCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync(bool isAdmin) {
            if (isAdmin) {
                var values = await _offerDiscountCollection.Find(x => true).ToListAsync();
                return _mapper.Map<List<ResultOfferDiscountDto>>(values);
            }
            var values2 = await _offerDiscountCollection
                .Find(x => x.Status == true)  // Status true olanları getir
                .SortBy(x => x.Order)         // Order alanına göre sırala
                .ToListAsync();
            return _mapper.Map<List<ResultOfferDiscountDto>>(values2);
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto) {

            var exists = await _offerDiscountCollection
                .Find(x => x.Order == createOfferDiscountDto.Order)
                .AnyAsync(); // daha önce bu order a (sıraya) sahip bir kayıt var mı

            if (!exists) {
                var value = _mapper.Map<OfferDiscount>(createOfferDiscountDto);
                await _offerDiscountCollection.InsertOneAsync(value);
            } else {
                throw new Exception("Bu sıra numarasına sahip bir kayıt zaten mevcut.");
            }
        }

        public async Task DeleteOfferDiscountAsync(string id) {
            await _offerDiscountCollection.DeleteOneAsync(x => x.OfferDiscountId == id);
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto) {
            var values = _mapper.Map<OfferDiscount>(updateOfferDiscountDto);
            await _offerDiscountCollection.FindOneAndReplaceAsync(x => x.OfferDiscountId == updateOfferDiscountDto.OfferDiscountId, values);
        }

        public async Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id) {
            var values = await _offerDiscountCollection.Find<OfferDiscount>(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOfferDiscountDto>(values);
        }

        public async Task OfferDiscountChangeStatusToTrue(string id) {
            var value = await _offerDiscountCollection.Find<OfferDiscount>(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
            value.Status = true;
            await _offerDiscountCollection.ReplaceOneAsync(x => x.OfferDiscountId == id, value);
        }

        public async Task OfferDiscountChangeStatusToFalse(string id) {
            var value = await _offerDiscountCollection.Find<OfferDiscount>(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
            value.Status = false;
            await _offerDiscountCollection.ReplaceOneAsync(x => x.OfferDiscountId == id, value);
        }
    }
}
