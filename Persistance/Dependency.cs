using Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class Dependency
    {
        private const string ConnectionString = "Server =.; DataBase = Local; UID = sa; PWD = !QAZ2wsx; Trusted_Connection = True; TrustServerCertificate = True";

        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services)
        {
            services.AddDbContext<ShopDbContext>(option =>
                           option.UseSqlServer(ConnectionString));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;

        }
    }
}
