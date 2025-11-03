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
using Contracts.Cache;

namespace Application.UseCases
{
    public class CustomerUsecase:BaseUsecase<CustomerEntity>, ICustomerUsecase
    {
        #region prorperties
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogService _logger;
        //private readonly ICacheService _cache;
        #endregion
        #region constructor
        public CustomerUsecase(ICustomerRepository customerRepository, ILogService logger) : base(customerRepository,logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            // _cache = cache;
        }
        #endregion
        #region methods
        public async Task<Result> IsEmailUniqueAsync(string email)
        {
           return await _customerRepository.IsEmailUniqueAsync(email) ? Result.Ok():Result.Fail("E-mail is already in use. Choose another one");
        }
        public async Task<Result<List<CustomerEntity>>> BirthdayVerification()
        {
            try
            {
                await _logger.InfoAsync("Fetching customers with birthdays today...");
                var customers = await _customerRepository.BirthdayCustomers();
                if (customers == null || customers.Count == 0)
                {
                    await _logger.WarnAsync("No customers have birthdays today.");
                    return Result<List<CustomerEntity>>.Fail("No customers have birthdays today.");
                }
                await _logger.InfoAsync($"Found {customers.Count} customers with birthdays today.");
                return Result<List<CustomerEntity>>.Ok(customers);
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("Failed to fetch birthday customers.", ex);
                return Result<List<CustomerEntity>>.Fail("Failed to fetch birthday customers.");
            }
        }
        public async Task<Result<List<CustomerEntity>>> GetCustomerWithSells()
        {
            try
            {
                await _logger.InfoAsync("Fetching customers with sells...");
                var customers = await _customerRepository.GetCustomersWithSells();

                if (customers == null || customers.Count == 0)
                {
                    await _logger.WarnAsync("No customers with sells were found.");
                    return Result<List<CustomerEntity>>.Fail("No customers with sells were found.");
                }

                await _logger.InfoAsync($"Found {customers.Count} customers with sells.");
                return Result<List<CustomerEntity>>.Ok(customers);
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("Failed to fetch customers with sells.", ex);
                return Result<List<CustomerEntity>>.Fail("Failed to fetch customers with sells.");
            }
        }
        public async Task<Result<List<CustomerEntity>>> GetCustomersByCityAsync(string city)
        {
            try
            {
                await _logger.InfoAsync($"Getting Customers by city {city}");
                var customers = await _customerRepository.GetCustomersByCityAsync(city);
                if (customers == null || customers.Count == 0)
                {
                    await _logger.WarnAsync($"There are no customers living in this city.");
                    return Result<List<CustomerEntity>>.Fail("There are no customers living in this city.");
                }
                return Result<List<CustomerEntity>>.Ok(customers);
            }
            catch (Exception ex) 
            {
                await _logger.ErrorAsync($"Falied to find users living in this city.",ex);
                return Result<List<CustomerEntity>>.Fail("Falied to find users living in this city.");
            }
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
 