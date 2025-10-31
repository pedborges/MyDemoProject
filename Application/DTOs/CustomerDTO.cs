using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerRole { get; set; } = string.Empty;
        public string CustomerPassword { get; set; } = string.Empty;
        public int CustomerAge { get; set; }
        public string CustomerCity { get; set; } = string.Empty;
        public string CustomerCountry { get; set; } = string.Empty;
        public DateTime CustomerBirthday { get; set; }
        public ICollection<SellEntity> Sells { get; set; } = new List<SellEntity>();

        public static implicit operator CustomerDTO(Domain.Entities.CustomerEntity entity)
        {
            return new CustomerDTO
            {
                CustomerId = entity.Id,
                CustomerName = entity.CustomerName,
                CustomerEmail = entity.CustomerEmail,
                CustomerRole = entity.CustomerRole,
                CustomerAge = entity.CustomerAge,
                CustomerCity = entity.CustomerCity,
                CustomerCountry = entity.CustomerCountry,
                CustomerBirthday = entity.CustomerBirthday,
                Sells= entity.Sells

            };
        }
        public static implicit operator CustomerEntity(CustomerDTO entityDTO)
        {
            return new CustomerEntity
            {
                Id = entityDTO.CustomerId,
                CustomerName = entityDTO.CustomerName,
                CustomerEmail = entityDTO.CustomerEmail,
                CustomerRole = entityDTO.CustomerRole,
                CustomerPassword = entityDTO.CustomerPassword,
                CustomerAge = entityDTO.CustomerAge,
                CustomerCity = entityDTO.CustomerCity,
                CustomerCountry = entityDTO.CustomerCountry,
                CustomerBirthday = entityDTO.CustomerBirthday,
                Sells = entityDTO.Sells
            };
        }
    }
}
