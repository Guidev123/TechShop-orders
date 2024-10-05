using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TechShop.Application.DTOs;
using TechShop.Domain.Repositories;

namespace TechShop.Application.Subscribers
{
    public class PaymentAcceptedSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QUEUE = "order-service/payment-accepted";
        private const string EXCHANGE = "order-service";
        private const string ROUNTING_KEY = "payment-accepted";
        public PaymentAcceptedSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("order-service-payment-accepted-subscriber");

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(EXCHANGE, "topic", true);
            _channel.QueueDeclare(QUEUE, true, false, false, null);
            _channel.QueueBind(QUEUE, "payment-service", ROUNTING_KEY);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<PaymentAcceptedDTO>(contentString) ?? new();

                var result = await UpdateOrder(message);
                if (result)
                {
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> UpdateOrder(PaymentAcceptedDTO paymentAcceptedDTO)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();
                if(orderRepository is null) return false;

                var order = await orderRepository.GetByIdAsync(paymentAcceptedDTO.Id);
                if (order is null) return false;

                order.SetAsCompleted();

                await orderRepository.UpdateAsync(order);

                return true;
            }
        }
    }
}
