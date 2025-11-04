using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record SellDTO
    {
        public Guid SellId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid TransactionId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SellDate { get; set; }
        public List<SellEntity>? sells { get; set; } = new List<SellEntity>();

        public static implicit operator SellDTO(SellEntity entity)
        {
            return new SellDTO
            {
                SellId=entity.Id ,
                CustomerId = entity.CustomerId,
                ProductId = entity.ProductId,
                TransactionId = entity.TransactionId,
                Quantity = entity.Quantity,
                TotalPrice = entity.TotalPrice,
                SellDate = entity.SellDate,                
            };
        }
        public static implicit operator SellEntity(SellDTO entityDTO)
        {
            return new SellEntity
            {
                Id = entityDTO.SellId,
                CustomerId = entityDTO.CustomerId,
                ProductId = entityDTO.ProductId,
                TransactionId = entityDTO.TransactionId == Guid.Empty ? Guid.NewGuid() : entityDTO.TransactionId,
                Quantity = entityDTO.Quantity,
                TotalPrice = entityDTO.TotalPrice,
                SellDate = entityDTO.SellDate,                

            };
        }
    }
}

