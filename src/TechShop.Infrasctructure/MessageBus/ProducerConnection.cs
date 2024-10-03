using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Infrasctructure.MessageBus
{
    public class ProducerConnection(IConnection connection)
    {
        public IConnection Connection { get; set; } = connection;
    }
}
