namespace HouseholdExpenseManager.Api.DTOs.Person.Request;

public sealed class CreatePersonRequest
{
    public required string Name { get; init; }

    public DateOnly BirthDate { get; init; }
}
