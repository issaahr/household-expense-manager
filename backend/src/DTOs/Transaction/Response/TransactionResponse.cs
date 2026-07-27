using HouseholdExpenseManager.Api.Enums;

namespace HouseholdExpenseManager.Api.DTOs.Transaction.Response;

public sealed class TransactionResponse
{
    public int Id { get; init; }

    public required string Description { get; init; }

    public decimal Amount { get; init; }

    public TransactionType Type { get; init; }

    public int PersonId { get; init; }
}
