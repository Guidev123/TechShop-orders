using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.Commands;

namespace TechShop.Application.Configuration
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddHandlers();
        }
        public static void AddHandlers(this IServiceCollection services) =>
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateOrder>()); 
    }
}
