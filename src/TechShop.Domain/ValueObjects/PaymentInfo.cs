using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Domain.ValueObjects
{
    public class PaymentInfo
    {
        public PaymentInfo(string cardNumber, string fullName, string expiration, string cvv)
        {
            CardNumber = cardNumber;
            FullName = fullName;
            Expiration = expiration;
            Cvv = cvv;
        }
        public string CardNumber { get; private set; }
        public string FullName { get; private set; }
        public string Expiration { get; private set; }
        public string Cvv { get; private set; }

        public override bool Equals(object? obj) =>
                   obj is PaymentInfo info &&
                   CardNumber == info.CardNumber &&
                   FullName == info.FullName &&
                   Expiration == info.Expiration &&
                   Cvv == info.Cvv;

        public override int GetHashCode() => HashCode.Combine(CardNumber, FullName, Expiration, Cvv);
    }
}
