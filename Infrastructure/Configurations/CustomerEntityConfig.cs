using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CustomerEntityConfig : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> b)
        {
            b.ToTable("Customers");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id)
             .ValueGeneratedOnAdd(); 

          
            b.Property(x => x.CustomerName).IsRequired().HasMaxLength(200);
            b.Property(x => x.CustomerEmail).IsRequired().HasMaxLength(200);
            b.Property(x => x.CustomerRole).HasMaxLength(100);
            b.Property(x => x.CustomerPassword).IsRequired().HasMaxLength(200); // (Don’t store plain text)
            b.Property(x => x.CustomerCity).HasMaxLength(120);
            b.Property(x => x.CustomerCountry).HasMaxLength(120);

            
            b.Property(x => x.CreatedAt)
             .HasDefaultValueSql("CURRENT_TIMESTAMP");
            b.Property(x => x.LastTimeUpdated)
             .HasDefaultValueSql("CURRENT_TIMESTAMP");

            
            b.HasIndex(x => x.CustomerEmail).IsUnique();
        }
    }
}
