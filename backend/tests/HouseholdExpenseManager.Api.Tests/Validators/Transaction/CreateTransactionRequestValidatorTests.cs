using FluentValidation.Results;
using HouseholdExpenseManager.Api.DTOs.Transaction.Request;
using HouseholdExpenseManager.Api.Enums;
using HouseholdExpenseManager.Api.Validators.Transaction;
using Xunit;

namespace HouseholdExpenseManager.Api.Tests.Validators.Transaction;

public class CreateTransactionRequestValidatorTests
{
    private readonly CreateTransactionRequestValidator validator = new();


    [Fact]
    public void Should_Pass_When_Request_Is_Valid()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = "Compra de mercado",
            Amount = 150,
            Type = TransactionType.Expense,
            PersonId = 1
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.True(result.IsValid);
    }


    [Fact]
    public void Should_Fail_When_Description_Is_Empty()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = "",
            Amount = 150,
            Type = TransactionType.Expense,
            PersonId = 1
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Description)
        );
    }


    [Fact]
    public void Should_Fail_When_Description_Has_Only_Spaces()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = "   ",
            Amount = 150,
            Type = TransactionType.Expense,
            PersonId = 1
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Description)
        );
    }


    [Fact]
    public void Should_Fail_When_Description_Is_Too_Short()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = "A",
            Amount = 150,
            Type = TransactionType.Expense,
            PersonId = 1
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Description)
        );
    }


    [Fact]
    public void Should_Fail_When_Description_Exceeds_Maximum_Length()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = new string('A', 501),
            Amount = 150,
            Type = TransactionType.Expense,
            PersonId = 1
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Description)
        );
    }


    [Fact]
    public void Should_Fail_When_Amount_Is_Not_Positive()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = "Compra",
            Amount = 0,
            Type = TransactionType.Expense,
            PersonId = 1
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.Amount)
        );
    }


    [Fact]
    public void Should_Fail_When_PersonId_Is_Invalid()
    {
        // Arrange
        CreateTransactionRequest request = new()
        {
            Description = "Compra",
            Amount = 100,
            Type = TransactionType.Expense,
            PersonId = 0
        };


        // Act
        ValidationResult result =
            validator.Validate(request);


        // Assert
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(request.PersonId)
        );
    }
}
