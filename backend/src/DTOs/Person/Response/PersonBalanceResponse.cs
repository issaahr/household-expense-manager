namespace HouseholdExpenseManager.Api.DTOs.Person.Response;

/// <summary>
/// Representa o resumo financeiro de uma pessoa.
/// </summary>
public sealed class PersonBalanceResponse
{
    public int PersonId { get; init; }

    public required string Name { get; init; }

    public decimal TotalIncome { get; init; }

    public decimal TotalExpense { get; init; }

    public decimal Balance =>
        TotalIncome - TotalExpense;
}
