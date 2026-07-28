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

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTransactions()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        Person person = new()
        {
            Name = "Maria",
            BirthDate = new DateOnly(1995, 1, 1)
        };

        context.People.Add(person);

        await context.SaveChangesAsync();

        context.Transactions.AddRange(
            new Transaction
            {
                Description = "Salário",
                Amount = 3000,
                Type = TransactionType.Income,
                PersonId = person.Id
            },
            new Transaction
            {
                Description = "Mercado",
                Amount = 250,
                Type = TransactionType.Expense,
                PersonId = person.Id
            }
        );

        await context.SaveChangesAsync();

        TransactionService service = new(context);


        // Act
        var result = await service.GetAllAsync();


        // Assert
        Assert.Equal(2, result.Count);

        Assert.Contains(result, transaction =>
            transaction.Description == "Salário");

        Assert.Contains(result, transaction =>
            transaction.Description == "Mercado");
    }


    [Fact]
    public async Task GetByPersonAsync_ShouldReturnOnlyPersonTransactions()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        Person maria = new()
        {
            Name = "Maria",
            BirthDate = new DateOnly(1995, 1, 1)
        };

        Person joao = new()
        {
            Name = "João",
            BirthDate = new DateOnly(1995, 1, 1)
        };

        context.People.AddRange(maria, joao);

        await context.SaveChangesAsync();


        context.Transactions.AddRange(
            new Transaction
            {
                Description = "Salário",
                Amount = 3000,
                Type = TransactionType.Income,
                PersonId = maria.Id
            },
            new Transaction
            {
                Description = "Mercado",
                Amount = 200,
                Type = TransactionType.Expense,
                PersonId = joao.Id
            }
        );

        await context.SaveChangesAsync();


        TransactionService service = new(context);


        // Act
        var result = await service.GetByPersonAsync(maria.Id);


        // Assert
        Assert.Single(result);

        Assert.Equal(
            "Salário",
            result[0].Description
        );

        Assert.Equal(
            maria.Id,
            result[0].PersonId
        );
    }


    [Fact]
    public async Task GetByPersonAsync_ShouldThrowException_WhenPersonDoesNotExist()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        TransactionService service = new(context);


        // Act + Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => service.GetByPersonAsync(999)
        );
    }


    [Fact]
    public async Task GetAllAsync_ShouldIncludePersonName()
    {
        // Arrange
        await using AppDbContext context = TestDbContextFactory.Create();

        Person person = new()
        {
            Name = "Maria",
            BirthDate = new DateOnly(1995, 1, 1)
        };

        context.People.Add(person);

        await context.SaveChangesAsync();


        context.Transactions.Add(
            new Transaction
            {
                Description = "Salário",
                Amount = 3000,
                Type = TransactionType.Income,
                PersonId = person.Id
            }
        );

        await context.SaveChangesAsync();


        TransactionService service = new(context);


        // Act
        var result = await service.GetAllAsync();


        // Assert
        Assert.Equal(
            "Maria",
            result[0].PersonName
        );
    }
}
