using Xunit;
using HouseholdExpenseManager.Api.Rules;

namespace HouseholdExpenseManager.Api.Tests.Rules;

public class PersonRulesTests
{
    [Fact]
    public void GetAge_Should_Return_Current_Age()
    {
        var birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-25)
            );

        int age = PersonRules.CalculateAge(birthDate);

        Assert.Equal(25, age);
    }


    [Fact]
    public void GetAge_Should_Decrease_When_Birthday_Has_Not_Happened()
    {
        var birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-25).AddDays(1)
            );

        int age = PersonRules.CalculateAge(birthDate);

        Assert.Equal(24, age);
    }


    [Fact]
    public void IsMinor_Should_Return_True_When_Under_18()
    {
        var birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-17)
            );

        bool result = PersonRules.IsMinor(birthDate);

        Assert.True(result);
    }


    [Fact]
    public void IsMinor_Should_Return_False_When_Adult()
    {
        var birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-18)
            );

        bool result = PersonRules.IsMinor(birthDate);

        Assert.False(result);
    }
}
