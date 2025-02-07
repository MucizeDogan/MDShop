namespace MDShop.DtoLayer.CatalogDtos.BrandDtos {
    public class UpdateBrandDto {
        public string BrandId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
