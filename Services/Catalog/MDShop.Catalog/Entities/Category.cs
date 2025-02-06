using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDShop.Catalog.Entities {
    public class Category {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Benzersiz olduğunu bildirmek için parametre
        public string CategoryID { get; set; } // MongoDB de id ler stribg guid değer aldığı için string yaptık. Ayrıca id olduğunu belirtmek için 2 tane attirubute var
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
