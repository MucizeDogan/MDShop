﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDShop.Catalog.Entities {
    public class Brand {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BrandId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
