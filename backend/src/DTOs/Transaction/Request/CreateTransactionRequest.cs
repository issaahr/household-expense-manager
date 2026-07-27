using HouseholdExpenseManager.Api.Enums;

namespace HouseholdExpenseManager.Api.DTOs.Transaction.Request;

public sealed class CreateTransactionRequest
{
    public required string Description { get; init; }

    public decimal Amount { get; init; }

    public TransactionType Type { get; init; }

    public int PersonId { get; init; }
}
