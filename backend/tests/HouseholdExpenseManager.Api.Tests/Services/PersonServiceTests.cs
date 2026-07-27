using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.DTOs.Person.Response;
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
}
