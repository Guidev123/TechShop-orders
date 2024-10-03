using MediatR;
using TechShop.Domain.Repositories;
using TechShop.Infrasctructure.Configuration;
using TechShop.Infrasctructure.MessageBus;

namespace TechShop.Application.Commands.Handlers
{
    public class CreateOrderHandler(IOrderRepository orderRepository, IBusClient messageBus) : IRequestHandler<CreateOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IBusClient _messageBus = messageBus;
        public async Task<Guid> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = request.ToEntity();
            await _orderRepository.CreateAsync(order);

            foreach (var events in order.Events)
            {
                var routingKey = events.GetType().Name.ToDashCase();    
                _messageBus.SendMessage(events, routingKey, "order-service");
            }

            return order.Id;
        }
    }
}
