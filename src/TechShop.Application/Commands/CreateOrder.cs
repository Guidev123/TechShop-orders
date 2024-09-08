using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.DTOs;
using TechShop.Domain.Entities;
using TechShop.Domain.ValueObjects;

namespace TechShop.Application.Commands
{
    public class CreateOrder : IRequest<Guid>
    {
        public CustomerInputModel Customer { get; set; } = null!;
        public List<OrderItemInputModel> OrderItens { get; set; } = [];
        public DeliveryAddressInputModel DeliveryAddress { get; set; } = null!;
        public PaymentAddressInputModel PaymentAddress { get; set; } = null!;
        public PaymentInfoInputModel PaymentInfo { get; set; } = null!;

        public Order ToEntity() => new
                (new Customer(Customer.Id, Customer.Name, Customer.Email),
                new DeliveryAddress(DeliveryAddress.Street, DeliveryAddress.Number, DeliveryAddress.City, DeliveryAddress.State, DeliveryAddress.ZipCode),
                new PaymentAddress(PaymentAddress.Street, PaymentAddress.Number, PaymentAddress.City, PaymentAddress.State, PaymentAddress.ZipCode),
                new PaymentInfo(PaymentInfo.CardNumber, PaymentInfo.FullName, PaymentInfo.Expiration, PaymentInfo.Cvv),
                OrderItens.Select(x => new OrderItem(x.ProductId, x.Quantity, x.Price)).ToList());
    }
    public class CustomerInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class OrderItemInputModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class DeliveryAddressInputModel
    {
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

    }
    public class PaymentAddressInputModel
    {
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
    public class PaymentInfoInputModel
    {
        public string CardNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public string Cvv { get; private set; } = string.Empty;
    }
}
