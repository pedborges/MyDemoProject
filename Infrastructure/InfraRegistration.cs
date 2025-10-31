using Domain.Contracts;
using Infrastructure.DbClient;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfraRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
           services.AddDbContext<DBClient>(options =>options.UseSqlite("Data Source=app.db"));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISellRepository, SellRepository>();
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            return services;
        }
    }
}
