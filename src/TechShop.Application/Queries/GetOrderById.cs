using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.DTOs;

namespace TechShop.Application.Queries
{
    public class GetOrderById : IRequest<OrderDTO>
    {
        public GetOrderById(Guid id) => Id = id;
        public Guid Id { get; }
    }
}
