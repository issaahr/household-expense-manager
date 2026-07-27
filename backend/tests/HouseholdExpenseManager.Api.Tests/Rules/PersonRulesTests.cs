using HouseholdExpenseManager.Api.Rules;
using Xunit;

namespace HouseholdExpenseManager.Api.Tests.Rules;

public class PersonRulesTests
{
    [Fact]
    public void CalculateAge_Should_Return_Current_Age()
    {
        // Arrange
        DateOnly birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-25)
            );


        // Act
        int age =
            PersonRules.CalculateAge(birthDate);


        // Assert
        Assert.Equal(
            25,
            age
        );
    }


    [Fact]
    public void CalculateAge_Should_Decrease_When_Birthday_Has_Not_Happened()
    {
        // Arrange
        DateOnly birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-25).AddDays(1)
            );


        // Act
        int age =
            PersonRules.CalculateAge(birthDate);


        // Assert
        Assert.Equal(
            24,
            age
        );
    }


    [Fact]
    public void IsMinor_Should_Return_True_When_Under_18()
    {
        // Arrange
        DateOnly birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-17)
            );


        // Act
        bool result =
            PersonRules.IsMinor(birthDate);


        // Assert
        Assert.True(result);
    }


    [Fact]
    public void IsMinor_Should_Return_False_When_Adult()
    {
        // Arrange
        DateOnly birthDate =
            DateOnly.FromDateTime(
                DateTime.Today.AddYears(-18)
            );


        // Act
        bool result =
            PersonRules.IsMinor(birthDate);


        // Assert
        Assert.False(result);
    }
}
