using Application.UseCases;
using Application.UsecasesContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Service;

namespace Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var Token = config["TokenData:SecretAPIKey"] ?? "falhou";
            services.AddScoped<ISellUsecase, SellUsecase>();
            services.AddScoped<ICustomerUsecase, CustomerUsecase>();
            services.AddScoped<IProductUsecase, ProductUsecase>();
            services.AddSingleton<IJWTService>(new JWTService(Token));
            return services;
        }
    }
}
