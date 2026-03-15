using DoomsdayTraining.Domain.Models;
using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Integrationstester för hela Doomsday-algoritmen.
/// Verifierar att hela kedjan (sekelankare → årets Doomsday → månadsreferens → slutresultat)
/// ger korrekt veckodag för kända historiska datum.
/// 
/// Vi kontrollerar mot .NETs inbyggda DayOfWeek för att säkerställa 100% korrekthet.
/// </summary>
public class DoomsdayAlgorithmTests
{
    /// <summary>
    /// Hjälpmetod som konverterar .NETs DayOfWeek till vår Weekday-enum.
    /// </summary>
    private static Weekday ToWeekday(DayOfWeek dow) => (Weekday)(int)dow;

    [Theory]
    [InlineData(2024, 4, 10)]   // 10 april 2024
    [InlineData(2024, 1, 1)]    // Nyårsdagen 2024
    [InlineData(2024, 12, 25)]  // Juldagen 2024
    [InlineData(2024, 2, 29)]   // Skottdagen 2024
    [InlineData(2000, 1, 1)]    // Millennieskiftet
    [InlineData(1987, 6, 15)]   // Slumpmässigt 80-talsdatum
    [InlineData(1900, 3, 1)]    // Icke-skottår sekelskifte
    [InlineData(2000, 2, 29)]   // Skottdag på sekelskifte (delbart med 400)
    [InlineData(1969, 7, 20)]   // Månlandningen
    [InlineData(1989, 11, 9)]   // Berlinmurens fall
    [InlineData(1776, 7, 4)]    // USA:s självständighetsdag
    public void GetWeekday_ShouldMatchDotNetDay_WhenGivenHistoricalDates(
        int year, int month, int day)
    {
        var date = new DateOnly(year, month, day);
        var expected = ToWeekday(date.DayOfWeek);

        var result = DoomsdayAlgorithm.GetWeekday(date);

        result.Should().Be(expected,
            $"{date:yyyy-MM-dd} ska vara {expected}");
    }

    [Fact]
    public void Calculate_ShouldReturnAllFiveSteps_WhenCalledWithDate()
    {
        var date = new DateOnly(2024, 4, 10);

        var result = DoomsdayAlgorithm.Calculate(date);

        result.Steps.Should().HaveCount(5,
            "Algoritmen ska producera exakt 5 steg");
    }

    [Fact]
    public void Calculate_ShouldReturnCorrectWeekday_WhenCalledWithDate()
    {
        var date = new DateOnly(2024, 4, 10);

        var result = DoomsdayAlgorithm.Calculate(date);

        result.Weekday.Should().Be(Weekday.Onsdag,
            "10 april 2024 är en onsdag");
    }

    [Fact]
    public void Calculate_ShouldPopulateAllStepFields_WhenCalledWithDate()
    {
        var date = new DateOnly(2024, 4, 10);

        var result = DoomsdayAlgorithm.Calculate(date);

        foreach (var step in result.Steps)
        {
            step.Title.Should().NotBeNullOrWhiteSpace();
            step.Explanation.Should().NotBeNullOrWhiteSpace();
            step.Calculation.Should().NotBeNullOrWhiteSpace();
            step.Result.Should().NotBeNullOrWhiteSpace();
        }
    }

    [Fact]
    public void Calculate_ShouldIdentifyLeapYear_WhenCalledWithLeapYearDate()
    {
        var date = new DateOnly(2024, 2, 29);

        var result = DoomsdayAlgorithm.Calculate(date);

        result.IsLeapYear.Should().BeTrue();
    }

    [Fact]
    public void Calculate_ShouldSetCorrectCenturyAnchor_WhenCalledWithDate()
    {
        var date = new DateOnly(2024, 6, 15);

        var result = DoomsdayAlgorithm.Calculate(date);

        result.CenturyAnchor.Should().Be(Weekday.Tisdag,
            "2000-talets sekelankare är Tisdag");
    }

    /// <summary>
    /// Masstest: kontrollerar 1000 slumpmässiga datum mot .NETs inbyggda beräkning.
    /// Detta ger oss extremt hög konfidens att algoritmen är korrekt implementerad.
    /// </summary>
    [Fact]
    public void GetWeekday_ShouldMatchDotNet_WhenTestedWith1000RandomDates()
    {
        var random = new Random(42); // Fast seed för reproducerbarhet

        for (int i = 0; i < 1000; i++)
        {
            var year = random.Next(1600, 2400);
            var month = random.Next(1, 13);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var day = random.Next(1, daysInMonth + 1);
            var date = new DateOnly(year, month, day);
            var expected = ToWeekday(date.DayOfWeek);

            var result = DoomsdayAlgorithm.GetWeekday(date);

            result.Should().Be(expected,
                $"Datum {date:yyyy-MM-dd} ska vara {expected} men var {result}");
        }
    }
}
