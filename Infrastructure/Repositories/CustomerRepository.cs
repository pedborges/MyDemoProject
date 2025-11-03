using Domain.Contracts;
using Domain.Entities;
using Infrastructure.DbClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class CustomerRepository:BaseRepository<CustomerEntity>,ICustomerRepository
    {
       
        public CustomerRepository(DBClient client):base(client) { }

        public async Task<bool> IsEmailUniqueAsync(string email) => !await _set.AnyAsync(c => c.CustomerEmail == email);

        public async Task<List<CustomerEntity>> BirthdayCustomers()
        {
            var today = DateTime.UtcNow.Date;
            return await _set.AsNoTracking()
                .Where(c =>  c.CustomerBirthday.Month == today.Month
                            && c.CustomerBirthday.Day == today.Day)
                .ToListAsync();
        }

        public async Task<List<CustomerEntity>> GetCustomersByCityAsync(string city)=> await _set.AsNoTracking().
            Where(c => c.CustomerCity != null && c.CustomerCity == city).ToListAsync();

        public async Task<List<CustomerEntity>> GetCustomersWithSells() => await _set.AsNoTracking().Include(s=>s.Sells).ToListAsync();

        public async Task<CustomerEntity> ValidateUser(CustomerEntity login)
        {
           var user = await _set.AsNoTracking().Where(c => c.CustomerEmail == login.CustomerEmail && c.CustomerPassword == login.CustomerPassword).FirstOrDefaultAsync();
           return user ?? new CustomerEntity();
        }
    }
}
