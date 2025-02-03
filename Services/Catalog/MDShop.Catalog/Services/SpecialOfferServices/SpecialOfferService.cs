using AutoMapper;
using MDShop.Catalog.Dtos.FeatureSliderDtos;
using MDShop.Catalog.Dtos.SpecialOfferDtos;
using MDShop.Catalog.Entities;
using MDShop.Catalog.Settings;
using Microsoft.VisualBasic;
using MongoDB.Driver;

namespace MDShop.Catalog.Services.SpecialOfferServices {
    public class SpecialOfferService : ISpecialOfferService {
        private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
        private readonly IMapper _mapper;
        public SpecialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings) {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync() {
            //var values = await _specialOfferCollection.Find(x => true).ToListAsync();
            //return _mapper.Map<List<ResultSpecialOfferDto>>(values);

            var values = await _specialOfferCollection
                .Find(x => x.Status == true)  // Status true olanları getir
                .SortBy(x => x.Order)         // Order alanına göre sırala
                .ToListAsync();
            return _mapper.Map<List<ResultSpecialOfferDto>>(values);
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto) {
            var value = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _specialOfferCollection.InsertOneAsync(value);
        }

        public async Task DeleteSpecialOfferAsync(string id) {
            await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferId == id);
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto) {
            var values = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
            await _specialOfferCollection.FindOneAndReplaceAsync(x => x.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, values);
        }

        public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id) {
            var values = await _specialOfferCollection.Find<SpecialOffer>(x => x.SpecialOfferId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdSpecialOfferDto>(values);
        }

        public async Task SpecialOfferChangeStatusToTrue(string id) {
            var value = await _specialOfferCollection.Find<SpecialOffer>(x => x.SpecialOfferId == id).FirstOrDefaultAsync();
            value.Status = true;
            await _specialOfferCollection.ReplaceOneAsync(x => x.SpecialOfferId == id, value);
        }

        public async Task SpecialOfferChangeStatusToFalse(string id) {
            var value = await _specialOfferCollection.Find<SpecialOffer>(x => x.SpecialOfferId == id).FirstOrDefaultAsync();
            value.Status = false;
            await _specialOfferCollection.ReplaceOneAsync(x => x.SpecialOfferId == id, value);
        }
    }
}
