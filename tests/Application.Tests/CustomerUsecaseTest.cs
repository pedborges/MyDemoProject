
using Application.UseCases;
using Domain.Contracts;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Org.BouncyCastle.Crypto;
namespace Application.Tests;

public class CustomerUsecaseTest
{
    [Fact]
    public async Task GetAllAsync_returns_customers_from_repo()
    {
        //create mock repo from ICustomerRepository
        var repo = new Mock<ICustomerRepository>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<CustomerEntity>
        {
            new() { CustomerName = "Pedro" }
        });

        var usecase = new CustomerUsecase(repo.Object); // here we inject the mock repo to satisfy the constructor
        var result = await usecase.GetAllAsync();

        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(1);
        result.Data![0].CustomerName.Should().Be("Pedro");
    }
    [Fact]
    public async Task GetByCity_returns_customers_from_repo()
    {
        var repo = new Mock<ICustomerRepository>();
        repo.Setup(r => r.GetCustomersByCityAsync("Londrina")).ReturnsAsync(new List<CustomerEntity>
        {
            new() { CustomerName = "Pedro", CustomerCity="Londrina", },
            new() { CustomerName = "Contoso", CustomerCity="Londrina", },
        });
        var usecase = new CustomerUsecase(repo.Object);
        var result = await usecase.GetCustomersByCityAsync("Londrina");
        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(2);
        result.Data![0].CustomerCity.Should().Be("Londrina");
    }
    [Fact]
    public async Task Insert_returns_new_customer()
    {
        var repo = new Mock<ICustomerRepository>();
        repo.Setup(r => r.AddAsync(It.IsAny<CustomerEntity>())).ReturnsAsync(new CustomerEntity
        {
            Id = Guid.NewGuid(),
            CustomerName = "Pedro",
            CustomerEmail = "pedropinguellihotmail.com"
        });
        var customerUsecase = new CustomerUsecase(repo.Object);
        var result = await customerUsecase.AddAsync(new CustomerEntity
        {
            Id = Guid.NewGuid(),
            CustomerName = "Pedro",
            CustomerEmail = "pedropinguellihotmail.com"
        });
        result.Success.Should().BeTrue();
        result.Data!.CustomerName.Should().Be("Pedro");
        result.Data!.CustomerEmail.Should().Be("pedropinguellihotmail.com");
        result.Data!.Id.Should().NotBeEmpty();
    }
}
 