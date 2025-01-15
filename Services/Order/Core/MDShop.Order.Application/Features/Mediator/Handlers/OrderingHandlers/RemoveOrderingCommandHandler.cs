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
    public class RemoveOrderingCommandHandler : IRequestHandler<RemoveOrderingCommand> {
        private readonly IRepository<Ordering> _repository;

        public RemoveOrderingCommandHandler(IRepository<Ordering> repository) {
            _repository = repository;
        }

        public async Task Handle(RemoveOrderingCommand request, CancellationToken cancellationToken) {
            var value = await _repository.GetByIdAsync(request.Id);
            if (value != null) {
                await _repository.DeleteAsync(value);
            }
            
        }
    }
}
