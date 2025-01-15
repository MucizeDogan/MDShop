using MDShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MDShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MDShop.Order.Application.Interfaces;
using MDShop.Order.Domain.Entities;

namespace MDShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers {
    public class GetOrderDetailByIdQueryHandler {
        private readonly IRepository<OrderDetail> _repository;
        public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository) {
            _repository = repository;
        }
        public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query) {
            var values = await _repository.GetByIdAsync(query.Id);
            return new GetOrderDetailByIdQueryResult {
                OrderDetailId = values.OrderDetailId,
                ProductAmount = values.ProductAmount,
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                OrderingId = values.OrderingId,
                ProductPrice = values.ProductPrice,
                ProductTotalPrice = values.ProductTotalPrice
            };
        }
    }
}
