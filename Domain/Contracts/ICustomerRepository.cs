using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ICustomerRepository:IBaseRepository<CustomerEntity>
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task<List<CustomerEntity>> GetCustomersWithSells();
        Task<List<CustomerEntity>> BirthdayCustomers(); //This method can be within a lambda function to get the customers whose birthday is today and then send them an email.
        Task<List<CustomerEntity>> GetCustomersByCityAsync(string city);
        Task<CustomerEntity> ValidateUser(CustomerEntity login);
    }
}
