using MDShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MDShop.Order.Application.Interfaces;
using MDShop.Order.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers {
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommand> {
        private readonly IRepository<Ordering> _repository;

        public UpdateOrderingCommandHandler(IRepository<Ordering> repository) {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken) {
            var value = await _repository.GetByIdAsync(request.OrderingId);
            value.OrderDate = request.OrderDate;
            value.TotalPrice = request.TotalPrice;
            value.UserId = request.UserId;
            await _repository.UpdateAsync(value);
        }
    }
}
