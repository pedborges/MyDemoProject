using Contracts.Cache;
using Domain.Contracts;
using Infrastructure.Cache;
using Infrastructure.DbClient;
using Infrastructure.Log;
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
            // services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped(typeof(ILogService), typeof(LogService<>));
            services.AddScoped<ISellRepository, SellRepository>();
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            return services;
        }
    }
}
