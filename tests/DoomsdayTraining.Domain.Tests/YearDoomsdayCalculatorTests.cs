using DoomsdayTraining.Domain.Models;
using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Tester för årets Doomsday-beräkning.
/// Verifierar med kända datum som vi kan dubbelkolla mot en kalender.
/// </summary>
public class YearDoomsdayCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnTorsdag_When2024()
    {
        // 2024: sekelankare = Tisdag, y=24, 24/12=2 rest 0, 0/4=0
        // 2 + 2 + 0 + 0 = 4 → Torsdag
        // Verifiering: 4 april 2024 = Torsdag ✓
        var result = YearDoomsdayCalculator.Calculate(2024, Weekday.Tisdag);

        result.Should().Be(Weekday.Torsdag);
    }

    [Fact]
    public void Calculate_ShouldReturnLördag_When1987()
    {
        // 1987: sekelankare = Onsdag, y=87, 87/12=7 rest 3, 3/4=0
        // 3 + 7 + 3 + 0 = 13, 13 mod 7 = 6 → Lördag
        var result = YearDoomsdayCalculator.Calculate(1987, Weekday.Onsdag);

        result.Should().Be(Weekday.Lördag);
    }

    [Fact]
    public void Calculate_ShouldReturnTisdag_When2000()
    {
        // 2000: sekelankare = Tisdag, y=0, 0/12=0 rest 0, 0/4=0
        // 2 + 0 + 0 + 0 = 2 → Tisdag (sekelankaret! Perfekt.)
        var result = YearDoomsdayCalculator.Calculate(2000, Weekday.Tisdag);

        result.Should().Be(Weekday.Tisdag);
    }

    [Fact]
    public void Calculate_ShouldReturnOnsdag_When1900()
    {
        // 1900: sekelankare = Onsdag, y=0
        // 3 + 0 + 0 + 0 = 3 → Onsdag
        var result = YearDoomsdayCalculator.Calculate(1900, Weekday.Onsdag);

        result.Should().Be(Weekday.Onsdag);
    }

    [Fact]
    public void Calculate_ShouldReturnFredag_When2025()
    {
        // 2025: sekelankare = Tisdag, y=25, 25/12=2 rest 1, 1/4=0
        // 2 + 2 + 1 + 0 = 5 → Fredag
        var result = YearDoomsdayCalculator.Calculate(2025, Weekday.Tisdag);

        result.Should().Be(Weekday.Fredag);
    }

    [Theory]
    [InlineData(2024, Weekday.Torsdag)]
    [InlineData(2023, Weekday.Tisdag)]
    [InlineData(2022, Weekday.Måndag)]
    [InlineData(2021, Weekday.Söndag)]
    [InlineData(2020, Weekday.Lördag)]
    public void CalculateFromYear_ShouldReturnCorrectDoomsday_WhenGivenRecentYears(
        int year, Weekday expected)
    {
        var result = YearDoomsdayCalculator.Calculate(year);

        result.Should().Be(expected);
    }

    [Fact]
    public void GetComponents_ShouldReturnCorrectBreakdown_When2024()
    {
        var (quotient, remainder, leapAdj, doomsday) =
            YearDoomsdayCalculator.GetComponents(2024, Weekday.Tisdag);

        quotient.Should().Be(2);
        remainder.Should().Be(0);
        leapAdj.Should().Be(0);
        doomsday.Should().Be(Weekday.Torsdag);
    }
}
