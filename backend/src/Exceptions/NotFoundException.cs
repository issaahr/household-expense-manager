namespace HouseholdExpenseManager.Api.Exceptions;

/// <summary>
/// Representa um erro quando um recurso solicitado não existe.
/// </summary>
public sealed class NotFoundException(string message) : Exception(message)
{
}
