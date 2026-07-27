namespace HouseholdExpenseManager.Api.Exceptions;

/// <summary>
/// Representa um erro de negócio.
/// </summary>
public sealed class BusinessException(string message) : Exception(message)
{
}
