using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Infrasctructure.MessageBus
{
    public interface IBusClient
    {
        void SendMessage(object message, string routingKey, string exchange);
    }
}
