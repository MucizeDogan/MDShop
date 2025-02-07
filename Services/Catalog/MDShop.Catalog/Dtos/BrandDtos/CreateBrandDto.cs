namespace MDShop.Catalog.Dtos.BrandDtos {
    public class CreateBrandDto {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
