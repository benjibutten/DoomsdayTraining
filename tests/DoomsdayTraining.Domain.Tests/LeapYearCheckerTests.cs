using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Tester för skottårskontrollen.
/// Vi testar alla tre regler: delbart med 4, 100, och 400.
/// </summary>
public class LeapYearCheckerTests
{
    [Theory]
    [InlineData(2024)]
    [InlineData(2020)]
    [InlineData(2016)]
    [InlineData(1996)]
    [InlineData(1904)]
    public void IsLeapYear_ShouldReturnTrue_WhenDivisibleBy4ButNot100(int year)
    {
        LeapYearChecker.IsLeapYear(year).Should().BeTrue(
            $"{year} är delbart med 4 men inte med 100, alltså skottår");
    }

    [Theory]
    [InlineData(1900)]
    [InlineData(1800)]
    [InlineData(2100)]
    [InlineData(2200)]
    public void IsLeapYear_ShouldReturnFalse_WhenDivisibleBy100ButNot400(int year)
    {
        LeapYearChecker.IsLeapYear(year).Should().BeFalse(
            $"{year} är delbart med 100 men inte 400, alltså INTE skottår");
    }

    [Theory]
    [InlineData(2000)]
    [InlineData(1600)]
    [InlineData(2400)]
    public void IsLeapYear_ShouldReturnTrue_WhenDivisibleBy400(int year)
    {
        LeapYearChecker.IsLeapYear(year).Should().BeTrue(
            $"{year} är delbart med 400, alltså skottår");
    }

    [Theory]
    [InlineData(2023)]
    [InlineData(2019)]
    [InlineData(1999)]
    [InlineData(1901)]
    public void IsLeapYear_ShouldReturnFalse_WhenNotDivisibleBy4(int year)
    {
        LeapYearChecker.IsLeapYear(year).Should().BeFalse(
            $"{year} är inte ens delbart med 4, alltså INTE skottår");
    }
}
