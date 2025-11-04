using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ProductDTO
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }

        public static implicit operator ProductDTO(ProductEntity entity)
        {
            return new ProductDTO
            {
                ProductID = entity.Id,
                ProductName = entity.ProductName,
                ProductDescription = entity.ProductDescription,
                ProductPrice = entity.ProductPrice,
                ProductStock = entity.ProductStock
            };
        }
        public static implicit operator ProductEntity(ProductDTO entityDTO)
        {
            return new ProductEntity
            {
                Id = entityDTO.ProductID,
                ProductName = entityDTO.ProductName,
                ProductDescription = entityDTO.ProductDescription,
                ProductPrice = entityDTO.ProductPrice,
                ProductStock = entityDTO.ProductStock
            };
        }
    }

}