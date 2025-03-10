﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDShop.Catalog.Entities {
    public class ProductImage {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductImageID { get; set; }
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }
        public string ProductImage4 { get; set; }
        public string ProductId { get; set; }
        [BsonIgnore]
        public Product Product { get; set; }
    }
}
