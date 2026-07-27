using HouseholdExpenseManager.Api.Enums;

namespace HouseholdExpenseManager.Api.Entities;

public class Transaction
{
    public int Id { get; set; }

    public required string Description { get; set; }

    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public int PersonId { get; set; }

    public Person Person { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
