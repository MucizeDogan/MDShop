using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDShop.Catalog.Entities {
    public class Contact {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool isRead { get; set; }
        public DateTime SendDate { get; set; }
    }
}
