using HouseholdExpenseManager.Api.Enums;

namespace HouseholdExpenseManager.Api.Entities;

public class Person
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required DateOnly BirthDate { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

