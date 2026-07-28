using HouseholdExpenseManager.Api.Enums;

namespace HouseholdExpenseManager.Api.DTOs.Transaction.Response;

/// <summary>
/// Representa uma transação retornada pela API.
/// </summary>
public sealed class TransactionResponse
{
    public int Id { get; init; }

    public required string Description { get; init; }

    public decimal Amount { get; init; }

    public TransactionType Type { get; init; }

    public int PersonId { get; init; }

    public required string PersonName { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}
