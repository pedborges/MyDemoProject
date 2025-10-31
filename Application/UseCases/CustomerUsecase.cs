using Application.DTOs;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common;
using Application.UsecasesContracts;

namespace Application.UseCases
{
    public class CustomerUsecase:BaseUsecase<CustomerEntity>, ICustomerUsecase
    {
        #region prorperties
        private readonly ICustomerRepository _customerRepository;
        #endregion
        #region constructor
        public CustomerUsecase(ICustomerRepository customerRepository):base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion
        #region methods
        public async Task<Result> IsEmailUniqueAsync(string email)
        {
           return await _customerRepository.IsEmailUniqueAsync(email) ? Result.Ok():Result.Fail("E-mail is already in use. Choose another one");
        }
        public async Task<Result<List<CustomerEntity>>> BirthdayVerification()
        {
            var customers =await _customerRepository.BirthdayCustomers();
            if (customers == null || customers.Count == 0)
            {
                return Result<List<CustomerEntity>>.Fail("No customers have birthdays today.");
            }
            return Result<List<CustomerEntity>>.Ok(customers);
        }
        public async Task<Result<List<CustomerEntity>>> GetCustomerWithSells()
        {
            var customers = await _customerRepository.GetCustomersWithSells();
            if (customers == null || customers.Count == 0)
            {
                return Result<List<CustomerEntity>>.Fail("No customers have birthdays today.");
            }
            return Result<List<CustomerEntity>>.Ok(customers);
        }
        public async Task<Result<List<CustomerEntity>>> GetCustomersByCityAsync(string city)
        {
            var customers = await _customerRepository.GetCustomersByCityAsync(city);
            if (customers == null || customers.Count == 0)
            {
                return Result<List<CustomerEntity>>.Fail("There are no customers living in this city.");
            }
            return Result<List<CustomerEntity>>.Ok(customers);
        }
        public async Task<Result<CustomerEntity>> ValidateLogin(LoginDTO login)
        {
            var customerEntity = new CustomerEntity
            {
                CustomerEmail = login.Email,
                CustomerPassword = login.Password
            };
            var user = await _customerRepository.ValidateUser(customerEntity);
            if (user == null)
            {
                return Result<CustomerEntity>.Fail("Invalid credentials");
            }
            return Result<CustomerEntity>.Ok(user);
        }
        #endregion
    }
}
 