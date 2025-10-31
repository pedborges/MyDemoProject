using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SellEntityConfig : IEntityTypeConfiguration<SellEntity>
    {
        public void Configure(EntityTypeBuilder<SellEntity> b)
        {
            b.ToTable("Sells");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.CustomerId).IsRequired();
            b.Property(x => x.ProductId).IsRequired();
            b.Property(x => x.TransactionId).IsRequired();

            b.Property(x => x.Quantity)
                .IsRequired();

            b.Property(x => x.TotalPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            b.Property(x => x.SellDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property(x => x.LastTimeUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.HasIndex(x => x.CustomerId);
            b.HasIndex(x => x.ProductId);
            b.HasIndex(x => x.TransactionId);

            b.HasOne<CustomerEntity>()
            .WithMany(c =>c.Sells)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)   
            .HasConstraintName("FK_Sells_Customers_CustomerId");

            b.HasOne<ProductEntity>()
             .WithMany()
             .HasForeignKey(x => x.ProductId)
             .OnDelete(DeleteBehavior.Restrict)
             .HasConstraintName("FK_Sells_Products_ProductId");
        }
    }
}
