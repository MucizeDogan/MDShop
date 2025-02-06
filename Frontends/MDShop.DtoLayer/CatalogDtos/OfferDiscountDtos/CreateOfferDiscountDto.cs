namespace MDShop.DtoLayer.CatalogDtos.OfferDiscountDtos {
    public class CreateOfferDiscountDto {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
