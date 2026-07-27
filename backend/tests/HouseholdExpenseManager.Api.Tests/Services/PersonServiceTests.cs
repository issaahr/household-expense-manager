using Xunit;
using HouseholdExpenseManager.Api.DTOs.Person.Request;
using HouseholdExpenseManager.Api.Services;
using HouseholdExpenseManager.Api.Tests.Fixtures;
using HouseholdExpenseManager.Api.Data;
using HouseholdExpenseManager.Api.DTOs.Person.Response;

namespace HouseholdExpenseManager.Api.Tests.Services;

public class PersonServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Create_Person()
    {
        using AppDbContext context = TestDbContextFactory.Create();

        PersonService service = new(context);

        CreatePersonRequest request = new()
        {
            Name = "Maria Silva",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        PersonResponse response =
            await service.CreateAsync(request);


        Assert.NotEqual(0, response.Id);
        Assert.Equal(
            "Maria Silva",
            response.Name
        );
    }


    [Fact]
    public async Task CreateAsync_Should_Remove_Extra_Spaces_From_Name()
    {
        using AppDbContext context = TestDbContextFactory.Create();

        PersonService service = new(context);

        CreatePersonRequest request = new()
        {
            Name = "  Maria Silva  ",
            BirthDate = new DateOnly(2000, 1, 1)
        };


        PersonResponse response =
            await service.CreateAsync(request);


        Assert.Equal(
            "Maria Silva",
            response.Name
        );
    }


    [Fact]
    public async Task GetAllAsync_Should_Return_All_People()
    {
        using AppDbContext context = TestDbContextFactory.Create();

        context.People.Add(
            new()
            {
                Name = "Maria",
                BirthDate = new DateOnly(2000, 1, 1)
            }
        );

        await context.SaveChangesAsync();


        PersonService service = new(context);


        var response =
            await service.GetAllAsync();


        Assert.Single(response);
        Assert.Equal(
            "Maria",
            response[0].Name
        );
    }
}
