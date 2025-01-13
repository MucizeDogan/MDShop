using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Order.Application.Features.CQRS.Results.AddressResults {
    public class GetAddressByIdQueryResult { // burası 1 veri döndürecek ve parametresi olacak
        public int AddressId { get; set; }
        public string UserId { get; set; }
        public string District { get; set; } //ilçe
        public string City { get; set; }
        public string Detail { get; set; }
    }
}
