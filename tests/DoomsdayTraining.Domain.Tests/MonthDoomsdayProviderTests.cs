using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Tester för Doomsday-referensdatum per månad.
/// Verifierar att rätt datum returneras för varje månad,
/// med extra fokus på januari/februari-specialfallen.
/// </summary>
public class MonthDoomsdayProviderTests
{
    [Theory]
    [InlineData(4, 4)]    // April → 4:e
    [InlineData(6, 6)]    // Juni → 6:e
    [InlineData(8, 8)]    // Augusti → 8:e
    [InlineData(10, 10)]  // Oktober → 10:e
    [InlineData(12, 12)]  // December → 12:e
    public void GetDoomsdayForMonth_ShouldReturnSameAsMonth_WhenEvenMonth(
        int month, int expectedDay)
    {
        var result = MonthDoomsdayProvider.GetDoomsdayForMonth(month, isLeapYear: false);

        result.Should().Be(expectedDay);
    }

    [Theory]
    [InlineData(5, 9)]    // Maj → 9:e ("9-to-5")
    [InlineData(9, 5)]    // September → 5:e ("9-to-5" bakvänt)
    [InlineData(7, 11)]   // Juli → 11:e ("7-Eleven")
    [InlineData(11, 7)]   // November → 7:e ("7-Eleven" bakvänt)
    public void GetDoomsdayForMonth_ShouldReturn95711_WhenOddMonth(
        int month, int expectedDay)
    {
        var result = MonthDoomsdayProvider.GetDoomsdayForMonth(month, isLeapYear: false);

        result.Should().Be(expectedDay);
    }

    [Fact]
    public void GetDoomsdayForMonth_ShouldReturn7_WhenMarch()
    {
        var result = MonthDoomsdayProvider.GetDoomsdayForMonth(3, isLeapYear: false);

        result.Should().Be(7);
    }

    [Theory]
    [InlineData(false, 3)]   // Normalt år: 3 januari
    [InlineData(true, 4)]    // Skottår: 4 januari
    public void GetDoomsdayForMonth_ShouldHandleLeapYear_WhenJanuary(
        bool isLeapYear, int expectedDay)
    {
        var result = MonthDoomsdayProvider.GetDoomsdayForMonth(1, isLeapYear);

        result.Should().Be(expectedDay);
    }

    [Theory]
    [InlineData(false, 28)]  // Normalt år: 28 februari
    [InlineData(true, 29)]   // Skottår: 29 februari
    public void GetDoomsdayForMonth_ShouldHandleLeapYear_WhenFebruary(
        bool isLeapYear, int expectedDay)
    {
        var result = MonthDoomsdayProvider.GetDoomsdayForMonth(2, isLeapYear);

        result.Should().Be(expectedDay);
    }

    [Fact]
    public void GetAll_ShouldReturn12Months()
    {
        var all = MonthDoomsdayProvider.GetAll();

        all.Should().HaveCount(12);
    }

    [Fact]
    public void GetInfoForMonth_ShouldReturnMnemonic_WhenCalledForAnyMonth()
    {
        for (int month = 1; month <= 12; month++)
        {
            var info = MonthDoomsdayProvider.GetInfoForMonth(month);

            info.Mnemonic.Should().NotBeNullOrWhiteSpace(
                $"Månad {month} ska ha en minnesregel");
        }
    }

    [Theory]
    [InlineData(3, false, new[] { 7, 14, 21, 28 })]
    [InlineData(2, false, new[] { 7, 14, 21, 28 })]
    [InlineData(2, true, new[] { 1, 8, 15, 22, 29 })]
    public void GetValidDoomsdayDatesForMonth_ShouldReturnAllEquivalentDates(
        int month,
        bool isLeapYear,
        int[] expectedDays)
    {
        var result = MonthDoomsdayProvider.GetValidDoomsdayDatesForMonth(month, isLeapYear);

        result.Should().Equal(expectedDays);
    }

    [Theory]
    [InlineData(3, 7, false, true)]
    [InlineData(3, 14, false, true)]
    [InlineData(3, 21, false, true)]
    [InlineData(3, 28, false, true)]
    [InlineData(3, 13, false, false)]
    [InlineData(2, 29, false, false)]
    [InlineData(2, 29, true, true)]
    public void IsValidDoomsdayDateForMonth_ShouldValidateCandidateDay(
        int month,
        int day,
        bool isLeapYear,
        bool expectedResult)
    {
        var result = MonthDoomsdayProvider.IsValidDoomsdayDateForMonth(month, day, isLeapYear);

        result.Should().Be(expectedResult);
    }
}
