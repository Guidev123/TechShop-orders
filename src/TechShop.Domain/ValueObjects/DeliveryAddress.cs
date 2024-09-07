using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Domain.ValueObjects
{
    public class DeliveryAddress
    {
        public DeliveryAddress(string street, string number, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public override bool Equals(object? obj) =>
            obj is DeliveryAddress address &&
            Street == address.Street &&
            Number == address.Number &&
            City == address.City &&
            State == address.State &&
            ZipCode == address.ZipCode;

        public override int GetHashCode() => HashCode.Combine(Street, Number, City, State, ZipCode);
    }
}
