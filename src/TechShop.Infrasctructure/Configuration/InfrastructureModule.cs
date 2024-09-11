using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Repositories;
using TechShop.Infrasctructure.Persistence;
using TechShop.Infrasctructure.Persistence.Repositories;

namespace TechShop.Infrasctructure.Configuration
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddMongoMiddleware(services, configuration);
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
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
