namespace MDShop.Catalog.Dtos.FeatureDtos {
    public class CreateFeatureDto {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
