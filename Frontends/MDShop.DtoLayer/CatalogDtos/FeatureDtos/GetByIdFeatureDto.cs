﻿namespace MDShop.DtoLayer.CatalogDtos.FeatureDtos {
    public class GetByIdFeatureDto {
        public string FeatureId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
