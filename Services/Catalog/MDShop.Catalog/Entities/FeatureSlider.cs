using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDShop.Catalog.Entities {
    public class FeatureSlider {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Benzersiz olduğunu bildirmek için parametre
        public string FeatureSliderId { get; set; } // MongoDB de id ler string guid değer aldığı için string yaptık. Ayrıca id olduğunu belirtmek için 2 tane attirubute var
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
