namespace HouseholdExpenseManager.Api.DTOs.Person.Response;

/// <summary>
/// Representa uma pessoa cadastrada.
/// </summary>
public sealed class PersonResponse
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public int Age { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}
