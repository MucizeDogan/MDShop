namespace MDShop.Discount.Dtos {
    public class ResultDiscountCouponDto {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public string Rate { get; set; } // Kuponun indirim oranı
        public bool IsActive { get; set; }
        public DateTime ValidDate { get; set; }
    }
}
