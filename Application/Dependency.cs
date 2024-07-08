using Application.Categories.Service;
using Application.Category.Service;
using Application.Product.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class Dependency
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService , ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });
            return services;

        }
    }
}
