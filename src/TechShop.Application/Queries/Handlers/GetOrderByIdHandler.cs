using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.DTOs;
using TechShop.Domain.Repositories;

namespace TechShop.Application.Queries.Handlers
{
    public class GetOrderByIdHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderById, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<OrderDTO> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            var result = OrderDTO.FromEntity(order);

            return result;
        }
    }
}
