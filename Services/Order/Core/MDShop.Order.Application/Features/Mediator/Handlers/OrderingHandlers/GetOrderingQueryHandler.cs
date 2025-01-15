using MDShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MDShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MDShop.Order.Application.Features.Mediator.Results.OrderingResult;
using MDShop.Order.Application.Interfaces;
using MDShop.Order.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers {
    public class GetOrderingQueryHandler : IRequestHandler<GetOrderingQuery, List<GetOrderingQueryResult>> {
        private readonly IRepository<Ordering> _repository;

        public GetOrderingQueryHandler(IRepository<Ordering> repository) {
            _repository = repository;
        }

        public async Task<List<GetOrderingQueryResult>> Handle(GetOrderingQuery request, CancellationToken cancellationToken) {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetOrderingQueryResult {
                OrderingId = x.OrderingId,
                OrderDate = x.OrderDate,
                TotalPrice = x.TotalPrice,
                UserId = x.UserId
            }).ToList();
        }
    }
}
