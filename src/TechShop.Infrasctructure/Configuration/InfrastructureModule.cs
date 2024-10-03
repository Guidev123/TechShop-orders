using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Notifications;
using TechShop.Domain.Repositories;
using TechShop.Infrasctructure.MessageBus;
using TechShop.Infrasctructure.Notifications;
using TechShop.Infrasctructure.Persistence;
using TechShop.Infrasctructure.Persistence.Repositories;

namespace TechShop.Infrasctructure.Configuration
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
            AddMongoMiddleware(services, configuration);
            AddMessageBus(services);
        }
        public static void AddMongoMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MongoDbService>();

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection");
                return new MongoClient(connectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("dev-db");
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<INotificator, Notificator>();
        }

        public static void AddMessageBus(this IServiceCollection services)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection("order-service-producer");

            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IBusClient, RabbitMQClient>();
        }
        public static string ToDashCase(this string text)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));
            if (text.Length < 0) return text;

            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append(c);
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();

        }
    }
}
