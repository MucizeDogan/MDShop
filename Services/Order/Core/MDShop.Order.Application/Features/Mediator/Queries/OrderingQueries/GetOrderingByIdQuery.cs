using MDShop.Order.Application.Features.Mediator.Results.OrderingResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Order.Application.Features.Mediator.Queries.OrderingQueries {
    public class GetOrderingByIdQuery : IRequest<GetOrderingByIdQueryResult> {
        public int Id { get; set; }

        public GetOrderingByIdQuery(int id) {
            Id = id;
        }
    }
}
