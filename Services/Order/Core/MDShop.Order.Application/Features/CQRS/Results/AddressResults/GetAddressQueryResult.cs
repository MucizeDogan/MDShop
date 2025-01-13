using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Order.Application.Features.CQRS.Results.AddressResults {
    public class GetAddressQueryResult { // Address sınıfındaki propları tutacak ve listlememizi sağlar. // Hepsini listeleyecek
        public int AddressId { get; set; }
        public string UserId { get; set; }
        public string District { get; set; } //ilçe
        public string City { get; set; }
        public string Detail { get; set; }
    }
}
