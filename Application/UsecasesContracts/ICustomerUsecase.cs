using Application.Common;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsecasesContracts
{
    public interface ICustomerUsecase : IBaseUsecase<CustomerEntity>
    {
        Task<Result> IsEmailUniqueAsync(string email);
        Task<Result<List<CustomerEntity>>> BirthdayVerification();
        Task<Result<List<CustomerEntity>>> GetCustomersByCityAsync(string city);
        Task<Result<CustomerEntity>> ValidateLogin(LoginDTO login);
        Task<Result<List<CustomerEntity>>> GetCustomerWithSells();
    }
}
