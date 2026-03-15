using DoomsdayTraining.Domain.Models;
using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Tester för den slutgiltiga veckodagsberäkningen (avstånd från Doomsday-datum).
/// </summary>
public class WeekdayCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnDoomsday_WhenTargetIsDoomsdayDate()
    {
        // Om vi frågar om precis Doomsday-datumet ska vi få tillbaka Doomsday-veckodagen
        var result = WeekdayCalculator.Calculate(
            targetDay: 4, doomsdayInMonth: 4, yearDoomsday: Weekday.Torsdag);

        result.Should().Be(Weekday.Torsdag);
    }

    [Fact]
    public void Calculate_ShouldCountForward_WhenTargetIsAfterDoomsday()
    {
        // 10 april 2024: Doomsday = Torsdag, referens = 4/4
        // 10 - 4 = 6 dagar framåt → Torsdag + 6 = Onsdag
        var result = WeekdayCalculator.Calculate(
            targetDay: 10, doomsdayInMonth: 4, yearDoomsday: Weekday.Torsdag);

        result.Should().Be(Weekday.Onsdag);
    }

    [Fact]
    public void Calculate_ShouldCountBackward_WhenTargetIsBeforeDoomsday()
    {
        // 1 april 2024: Doomsday = Torsdag, referens = 4/4
        // 1 - 4 = -3 dagar → Torsdag - 3 = Måndag
        var result = WeekdayCalculator.Calculate(
            targetDay: 1, doomsdayInMonth: 4, yearDoomsday: Weekday.Torsdag);

        result.Should().Be(Weekday.Måndag);
    }

    [Fact]
    public void Calculate_ShouldWrapCorrectly_WhenCrossingSunday()
    {
        // Söndag(0) - 1 = Lördag(6)
        var result = WeekdayCalculator.Calculate(
            targetDay: 3, doomsdayInMonth: 4, yearDoomsday: Weekday.Söndag);

        result.Should().Be(Weekday.Lördag);
    }

    [Fact]
    public void GetDifferenceDescription_ShouldReturnZero_WhenSameDay()
    {
        var (diff, description) = WeekdayCalculator.GetDifferenceDescription(4, 4);

        diff.Should().Be(0);
        description.Should().Contain("Exakt");
    }

    [Fact]
    public void GetDifferenceDescription_ShouldReturnPositive_WhenTargetAfterDoomsday()
    {
        var (diff, description) = WeekdayCalculator.GetDifferenceDescription(10, 4);

        diff.Should().Be(6);
        description.Should().Contain("framåt");
    }

    [Fact]
    public void GetDifferenceDescription_ShouldReturnNegative_WhenTargetBeforeDoomsday()
    {
        var (diff, description) = WeekdayCalculator.GetDifferenceDescription(1, 4);

        diff.Should().Be(-3);
        description.Should().Contain("bakåt");
    }
}
