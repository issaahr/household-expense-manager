namespace HouseholdExpenseManager.Api.DTOs.Person.Response;

public sealed class PersonResponse
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public int Age { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}
