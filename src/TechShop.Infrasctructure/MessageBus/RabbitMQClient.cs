using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Infrasctructure.MessageBus
{
    public class RabbitMQClient(ProducerConnection producerConnection) : IBusClient
    {
        private readonly IConnection _connection = producerConnection.Connection;
        public void SendMessage(object message, string routingKey, string exchange)
        {
            var channel = _connection.CreateModel();
            var settings = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var payload = JsonConvert.SerializeObject(message);
            byte[] body = Encoding.UTF8.GetBytes(payload);

            channel.BasicPublish(exchange, routingKey, null, body);
        }
    }
}
