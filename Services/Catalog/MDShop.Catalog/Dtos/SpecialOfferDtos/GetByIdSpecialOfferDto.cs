﻿namespace MDShop.Catalog.Dtos.SpecialOfferDtos {
    public class GetByIdSpecialOfferDto {
        public string SpecialOfferId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
