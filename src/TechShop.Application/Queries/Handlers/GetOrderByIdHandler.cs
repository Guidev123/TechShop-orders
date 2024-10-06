using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.DTOs;
using TechShop.Domain.Repositories;
using TechShop.Infrasctructure.CacheStorage;

namespace TechShop.Application.Queries.Handlers
{
    public class GetOrderByIdHandler(IOrderRepository orderRepository, ICacheService cache) : IRequestHandler<GetOrderById, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICacheService _cacheService = cache;
        public async Task<OrderDTO> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var orderCache = await _cacheService.GetAsync<OrderDTO>(request.Id.ToString());
            if (orderCache is null)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id);
                if (order is null)
                    throw new ArgumentNullException(nameof(order));

                orderCache = OrderDTO.FromEntity(order);
                await _cacheService.SetAsync(request.Id.ToString(), orderCache);
            }

            return orderCache;
        }
    }
}
