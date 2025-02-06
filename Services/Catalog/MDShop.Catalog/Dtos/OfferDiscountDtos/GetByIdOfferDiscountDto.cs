namespace MDShop.Catalog.Dtos.OfferDiscountDtos {
    public class GetByIdOfferDiscountDto {
        public string OfferDiscountId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
