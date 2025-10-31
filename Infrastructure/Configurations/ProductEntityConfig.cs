using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> b)
        {
            b.ToTable("Products");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.ProductDescription)
                .HasMaxLength(500);

            b.Property(x => x.ProductPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            b.Property(x => x.ProductStock)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property(x => x.LastTimeUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
