using Xunit;
using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.Validators.Person;
using FluentValidation.Results;

namespace HouseholdExpenseManager.Api.Tests.Validators.Person;

public class CreatePersonRequestValidatorTests
{
    private readonly CreatePersonRequestValidator validator = new();


    [Fact]
    public void Should_Pass_When_Request_Is_Valid()
    {
        CreatePersonRequest request = new()
        {
            Name = "Maria Silva",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        ValidationResult result = validator.Validate(request);


        Assert.True(result.IsValid);
    }


    [Fact]
    public void Should_Fail_When_Name_Is_Empty()
    {
        CreatePersonRequest request = new()
        {
            Name = "",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        ValidationResult result = validator.Validate(request);


        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Name)
        );
    }


    [Fact]
    public void Should_Fail_When_Name_Is_Too_Short()
    {
        CreatePersonRequest request = new()
        {
            Name = "A",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        ValidationResult result = validator.Validate(request);


        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Name)
        );
    }


    [Fact]
    public void Should_Fail_When_BirthDate_Is_In_Future()
    {
        CreatePersonRequest request = new()
        {
            Name = "Maria Silva",
            BirthDate =
                DateOnly.FromDateTime(
                    DateTime.Today.AddDays(1)
                )
        };


        ValidationResult result = validator.Validate(request);


        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.BirthDate)
        );
    }
}
