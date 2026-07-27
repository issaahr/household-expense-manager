using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.DTOs.Transaction.Request;
using HouseholdExpenseManager.Api.Entities;
using HouseholdExpenseManager.Api.Enums;
using HouseholdExpenseManager.Api.Services;
using HouseholdExpenseManager.Api.Tests.Fixtures;
using HouseholdExpenseManager.Api.Exceptions;
using Xunit;

namespace HouseholdExpenseManager.Api.Tests.Services;

public sealed class TransactionServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateTransaction_WhenPersonIsAdult()
    {

        await using AppDbContext context = TestDbContextFactory.Create();

        Person person = new()
        {
            Name = "Maria",
            BirthDate = new DateOnly(1995, 1, 1)
        };

        context.People.Add(person);
        await context.SaveChangesAsync();

        TransactionService service = new(context);

        CreateTransactionRequest request = new()
        {
            Description = "Salário",
            Amount = 3000,
            Type = TransactionType.Income,
            PersonId = person.Id
        };


        // Act
        var result = await service.CreateAsync(request);


        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal(request.Description, result.Description);
        Assert.Equal(request.Amount, result.Amount);
        Assert.Equal(request.Type, result.Type);
        Assert.Equal(person.Id, result.PersonId);
    }


    [Fact]
    public async Task CreateAsync_ShouldCreateExpense_WhenPersonIsMinor()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        Person person = new()
        {
            Name = "João",
            BirthDate = DateOnly.FromDateTime(
                DateTime.UtcNow.AddYears(-15)
            )
        };

        context.People.Add(person);
        await context.SaveChangesAsync();

        TransactionService service = new(context);

        CreateTransactionRequest request = new()
        {
            Description = "Material escolar",
            Amount = 100,
            Type = TransactionType.Expense,
            PersonId = person.Id
        };


        // Act
        var result = await service.CreateAsync(request);


        // Assert
        Assert.Equal(TransactionType.Expense, result.Type);
        Assert.Equal(person.Id, result.PersonId);
    }


    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenMinorCreatesIncome()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        Person person = new()
        {
            Name = "João",
            BirthDate = DateOnly.FromDateTime(
                DateTime.UtcNow.AddYears(-15)
            )
        };

        context.People.Add(person);
        await context.SaveChangesAsync();

        TransactionService service = new(context);

        CreateTransactionRequest request = new()
        {
            Description = "Mesada",
            Amount = 200,
            Type = TransactionType.Income,
            PersonId = person.Id
        };


        // Act + Assert
        await Assert.ThrowsAsync<BusinessException>(
            () => service.CreateAsync(request)
        );
    }


    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenPersonDoesNotExist()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        TransactionService service = new(context);

        CreateTransactionRequest request = new()
        {
            Description = "Teste",
            Amount = 50,
            Type = TransactionType.Expense,
            PersonId = 999
        };


        // Act + Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => service.CreateAsync(request)
        );
    }
}
