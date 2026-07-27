namespace HouseholdExpenseManager.Api.DTOs.Person.Response;

/// <summary>
/// Representa o resumo financeiro geral das pessoas cadastradas.
/// </summary>
public sealed class PersonFinancialSummaryResponse
{
    public required IReadOnlyList<PersonBalanceResponse> People { get; init; }

    public decimal TotalIncome { get; init; }

    public decimal TotalExpense { get; init; }

    public decimal Balance =>
        TotalIncome - TotalExpense;
}
