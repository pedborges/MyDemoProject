using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbClient
{
    public class DBClient : DbContext
    {
        public DBClient(DbContextOptions<DBClient> options) : base(options) { }

        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<SellEntity> Sells => Set<SellEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBClient).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
            if (!options.IsConfigured)
                options.UseSqlite("Data Source=app.db");
        }
    }
}
