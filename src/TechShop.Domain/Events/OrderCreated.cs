using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.ValueObjects;

namespace TechShop.Domain.Events
{
    public class OrderCreated : IDomainEvent
    {
        public OrderCreated(Guid id, decimal totalPrice, PaymentInfo paymentInfo, string name, string email)
        {
            Id = id;
            TotalPrice = totalPrice;
            PaymentInfo = paymentInfo;
            Name = name;
            Email = email;
        }

        public Guid Id { get; }
        public decimal TotalPrice { get; }
        public PaymentInfo PaymentInfo { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
