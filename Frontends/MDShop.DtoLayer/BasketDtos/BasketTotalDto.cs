using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.DtoLayer.BasketDtos {
    public class BasketTotalDto {
        public string UserId { get; set; }
        public string? DiscountCode { get; set; }
        public string? DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }
    }
}
