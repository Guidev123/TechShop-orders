using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.Commands;
using TechShop.Application.Subscribers;

namespace TechShop.Application.Configuration
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddHandlers();
            services.AddSubscribers();
        }
        public static void AddHandlers(this IServiceCollection services) =>
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateOrder>());

        public static void AddSubscribers(this IServiceCollection services)
        {
            services.AddHostedService<PaymentAcceptedSubscriber>();
        }
    }
}
