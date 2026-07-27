namespace HouseholdExpenseManager.Api.DTOs.Person.Request;

/// <summary>
/// Representa a requisição para criar uma nova pessoa.
/// </summary>
public sealed class CreatePersonRequest
{
    public required string Name { get; init; }

    public DateOnly BirthDate { get; init; }
}
