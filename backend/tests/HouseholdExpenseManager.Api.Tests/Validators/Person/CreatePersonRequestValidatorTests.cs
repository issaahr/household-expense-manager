using FluentValidation.Results;
using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.Validators.Person;
using Xunit;

namespace HouseholdExpenseManager.Api.Tests.Validators.Person;

public class CreatePersonRequestValidatorTests
{
    private readonly CreatePersonRequestValidator validator = new();


    [Fact]
    public void Should_Pass_When_Request_Is_Valid()
    {
        // Arrange
        CreatePersonRequest request = new()
        {
            Name = "Maria Silva",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.True(result.IsValid);
    }


    [Fact]
    public void Should_Fail_When_Name_Is_Empty()
    {
        // Arrange
        CreatePersonRequest request = new()
        {
            Name = "",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Name)
        );
    }


    [Fact]
    public void Should_Fail_When_Name_Is_Too_Short()
    {
        // Arrange
        CreatePersonRequest request = new()
        {
            Name = "A",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Name)
        );
    }


    [Fact]
    public void Should_Fail_When_BirthDate_Is_In_Future()
    {
        // Arrange
        CreatePersonRequest request = new()
        {
            Name = "Maria Silva",
            BirthDate =
                DateOnly.FromDateTime(
                    DateTime.Today.AddDays(1)
                )
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.BirthDate)
        );
    }
}
