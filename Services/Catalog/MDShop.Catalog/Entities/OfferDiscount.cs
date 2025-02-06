﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MDShop.Catalog.Entities {
    public class OfferDiscount {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OfferDiscountId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
