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
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddRepositories(services);
            AddMongoMiddleware(services);
        }
        public static void AddMongoMiddleware(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbService>();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
