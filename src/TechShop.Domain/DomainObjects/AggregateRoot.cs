using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Events;

namespace TechShop.Domain.DomainObjects
{
    public class AggregateRoot : IEntity
    {
        public Guid Id { get; protected set; }

        private List<IDomainEvent> _events = [];
        protected void AddEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
            {
                _events = [];
            }

            _events.Add(domainEvent);
        } 
        public IEnumerable<IDomainEvent> Events => _events;
    }
}
