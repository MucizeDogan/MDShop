namespace MDShop.Catalog.Dtos.BrandDtos {
    public class GetByIdBrandDto {
        public string BrandId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
