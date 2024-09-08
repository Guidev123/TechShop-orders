using MediatR;
using TechShop.Domain.Repositories;

namespace TechShop.Application.Commands.Handlers
{
    public class CreateOrderHandler(IOrderRepository orderRepository) : IRequestHandler<CreateOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Guid> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = request.ToEntity();
            await _orderRepository.CreateAsync(order);
            return order.Id;
        }
    }
}
