using Domain.Entities;
using FluentAssertions;
using Infrastructure.DbClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests;

public class GetSellByCustomerID
{
    //first add the sellEntity and then get it using customer id method
    [Fact]
    public async Task GetSellsByCustomerId_returns_rows()
    {
        var options = new DbContextOptionsBuilder<DBClient>()
            .UseInMemoryDatabase("DbTests-" + Guid.NewGuid())
            .Options;

        using var db = new DBClient(options);
        db.Sells.Add(new SellEntity
        {
            CustomerId = Guid.Parse("76b25c07-a3c6-4e92-8876-ceb618bcf02b"),
            ProductId = Guid.NewGuid(),
            TransactionId = Guid.NewGuid(),
            Quantity = 1,
            TotalPrice = 2.0m,
            SellDate = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        var repo = new Infrastructure.Repositories.SellRepository(db);
        var list = await repo.GetSellsByCustomerIdAsync(Guid.Parse("76b25c07-a3c6-4e92-8876-ceb618bcf02b"));

        list.Should().HaveCount(1);
    }
}
