using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.DomainObjects;
using TechShop.Domain.Enums;
using TechShop.Domain.Events;
using TechShop.Domain.ValueObjects;

namespace TechShop.Domain.Entities
{
    public class Order : AggregateRoot
    {
        public Order(decimal totalPrice, Customer customer, DeliveryAddress deliveryAddress, PaymentAddress paymentAddress,
                     PaymentInfo paymentInfo, List<OrderItem> itens, DateTime createdAt, EOrderStatus status)
        {
            Id = Guid.NewGuid();
            TotalPrice = itens.Sum(x => x.Quantity * x.Price);
            Customer = customer;
            DeliveryAddress = deliveryAddress;
            PaymentAddress = paymentAddress;
            PaymentInfo = paymentInfo;
            Itens = itens;
            CreatedAt = DateTime.Now;
            AddEvent(new OrderCreated(Id, TotalPrice, paymentInfo, Customer.Name, Customer.Email));
            Status = EOrderStatus.Created;
        }

        public decimal TotalPrice { get; private set; }
        public Customer Customer { get; private set; }
        public DeliveryAddress DeliveryAddress { get; private set; }
        public PaymentAddress PaymentAddress { get; private set; }
        public PaymentInfo PaymentInfo { get; set; }
        public List<OrderItem> Itens { get; set; }
        public DateTime CreatedAt { get; private set; }
        public EOrderStatus Status { get; private set; }
        public void SetAsCompleted() => Status = EOrderStatus.Completed;
        public void SetAsRejected() => Status = EOrderStatus.Rejected;
    }
}
