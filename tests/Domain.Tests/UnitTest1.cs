using Domain.Entities;
using FluentAssertions;

namespace Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void UpdateTimestamp_sets_LastTimeUpdated()
    {
        var customer = new CustomerEntity();
        var before = customer.LastTimeUpdated;

        customer.UpdateTimestamp();

        customer.LastTimeUpdated.Should().BeAfter(before);
    }
}
