using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;

namespace TechShop.Application.DTOs
{
    public class OrderDTO
    {
        public OrderDTO(Guid id, decimal totalPrice, DateTime createdAt, string status)
        {
            Id = id;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
            Status = status;
        }

        public Guid Id { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Status { get; private set; }

        public static OrderDTO FromEntity(Order order) =>
            new(order.Id, order.TotalPrice, order.CreatedAt, order.Status.ToString());
    }
}
