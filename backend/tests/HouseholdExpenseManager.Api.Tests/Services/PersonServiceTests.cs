using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.DTOs.Person.Response;
using HouseholdExpenseManager.Api.Entities;
using HouseholdExpenseManager.Api.Enums;
using HouseholdExpenseManager.Api.Exceptions;
using HouseholdExpenseManager.Api.Services;
using HouseholdExpenseManager.Api.Tests.Fixtures;
using Xunit;

namespace HouseholdExpenseManager.Api.Tests.Services;

public class PersonServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Create_Person()
    {
        // Arrange
        using AppDbContext context =
            TestDbContextFactory.Create();

        PersonService service =
            new(context);

        CreatePersonRequest request = new()
        {
            Name = "Maria Silva",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        // Act
        PersonResponse response =
            await service.CreateAsync(request);


        // Assert
        Assert.NotEqual(
            0,
            response.Id
        );

        Assert.Equal(
            "Maria Silva",
            response.Name
        );
    }


    [Fact]
    public async Task CreateAsync_Should_Remove_Extra_Spaces_From_Name()
    {
        // Arrange
        using AppDbContext context =
            TestDbContextFactory.Create();

        PersonService service =
            new(context);

        CreatePersonRequest request = new()
        {
            Name = "  Maria Silva  ",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        // Act
        PersonResponse response =
            await service.CreateAsync(request);


        // Assert
        Assert.Equal(
            "Maria Silva",
            response.Name
        );
    }


    [Fact]
    public async Task GetAllAsync_Should_Return_All_People()
    {
        // Arrange
        using AppDbContext context =
            TestDbContextFactory.Create();

        context.People.Add(
            new()
            {
                Name = "Maria",
                BirthDate = new DateOnly(2000, 1, 1)
            }
        );

        await context.SaveChangesAsync();


        PersonService service =
            new(context);


        // Act
        IReadOnlyList<PersonResponse> response =
            await service.GetAllAsync();


        // Assert
        Assert.Single(response);

        Assert.Equal(
            "Maria",
            response[0].Name
        );
    }


    [Fact]
    public async Task DeleteAsync_Should_Remove_Person()
    {
        // Arrange
        using AppDbContext context =
            TestDbContextFactory.Create();

        var person = new HouseholdExpenseManager.Api.Entities.Person
        {
            Name = "Maria",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        context.People.Add(person);

        await context.SaveChangesAsync();


        PersonService service =
            new(context);


        // Act
        await service.DeleteAsync(person.Id);


        // Assert
        var deletedPerson =
            await context.People.FindAsync(person.Id);

        Assert.Null(deletedPerson);
    }


    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Person_Does_Not_Exist()
    {
        // Arrange
        using AppDbContext context =
            TestDbContextFactory.Create();

        PersonService service =
            new(context);


        // Act + Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => service.DeleteAsync(999)
        );
    }

    [Fact]
    public async Task GetFinancialSummaryAsync_Should_Return_Empty_When_No_People_Exist()
    {
        // Arrange
        using AppDbContext context = TestDbContextFactory.Create();

        PersonService service = new(context);

        // Act
        PersonFinancialSummaryResponse response =
            await service.GetFinancialSummaryAsync();

        // Assert
        Assert.Empty(response.People);
        Assert.Equal(0, response.TotalIncome);
        Assert.Equal(0, response.TotalExpense);
        Assert.Equal(0, response.Balance);
    }

    [Fact]
    public async Task GetFinancialSummaryAsync_Should_Calculate_Person_Balance()
    {
        // Arrange
        using AppDbContext context = TestDbContextFactory.Create();

        Person person = new()
        {
            Name = "Maria",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        context.People.Add(person);
        await context.SaveChangesAsync();

        context.Transactions.AddRange(
            new Transaction
            {
                Description = "Salário",
                Amount = 5000,
                Type = TransactionType.Income,
                PersonId = person.Id
            },
            new Transaction
            {
                Description = "Aluguel",
                Amount = 2000,
                Type = TransactionType.Expense,
                PersonId = person.Id
            },
            new Transaction
            {
                Description = "Mercado",
                Amount = 500,
                Type = TransactionType.Expense,
                PersonId = person.Id
            });

        await context.SaveChangesAsync();

        PersonService service = new(context);

        // Act
        PersonFinancialSummaryResponse response =
            await service.GetFinancialSummaryAsync();

        PersonBalanceResponse balance = response.People.Single();

        // Assert
        Assert.Equal(5000, balance.TotalIncome);
        Assert.Equal(2500, balance.TotalExpense);
        Assert.Equal(2500, balance.Balance);
    }

    [Fact]
    public async Task GetFinancialSummaryAsync_Should_Calculate_Global_Totals()
    {
        // Arrange
        using AppDbContext context = TestDbContextFactory.Create();

        Person maria = new()
        {
            Name = "Maria",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        Person joao = new()
        {
            Name = "João",
            BirthDate = new DateOnly(1998, 1, 1)
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
                Amount = 1000,
                Type = TransactionType.Expense,
                PersonId = maria.Id
            },
            new Transaction
            {
                Description = "Freelance",
                Amount = 2000,
                Type = TransactionType.Income,
                PersonId = joao.Id
            },
            new Transaction
            {
                Description = "Internet",
                Amount = 500,
                Type = TransactionType.Expense,
                PersonId = joao.Id
            });

        await context.SaveChangesAsync();

        PersonService service = new(context);

        // Act
        PersonFinancialSummaryResponse response =
            await service.GetFinancialSummaryAsync();

        // Assert
        Assert.Equal(5000, response.TotalIncome);
        Assert.Equal(1500, response.TotalExpense);
        Assert.Equal(3500, response.Balance);
    }
}
