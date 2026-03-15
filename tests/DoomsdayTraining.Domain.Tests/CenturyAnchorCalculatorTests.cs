using DoomsdayTraining.Domain.Models;
using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Tester för sekelankare-beräkningen.
/// Vi verifierar alla fyra ankare i 400-årscykeln samt formelns korrekthet.
/// </summary>
public class CenturyAnchorCalculatorTests
{
    [Theory]
    [InlineData(1800, Weekday.Fredag)]
    [InlineData(1899, Weekday.Fredag)]
    [InlineData(1850, Weekday.Fredag)]
    public void Calculate_ShouldReturnFredag_When1800Century(int year, Weekday expected)
    {
        CenturyAnchorCalculator.Calculate(year).Should().Be(expected,
            $"Alla år på 1800-talet ska ha Fredag som sekelankare");
    }

    [Theory]
    [InlineData(1900, Weekday.Onsdag)]
    [InlineData(1987, Weekday.Onsdag)]
    [InlineData(1999, Weekday.Onsdag)]
    public void Calculate_ShouldReturnOnsdag_When1900Century(int year, Weekday expected)
    {
        CenturyAnchorCalculator.Calculate(year).Should().Be(expected,
            $"Alla år på 1900-talet ska ha Onsdag som sekelankare");
    }

    [Theory]
    [InlineData(2000, Weekday.Tisdag)]
    [InlineData(2024, Weekday.Tisdag)]
    [InlineData(2099, Weekday.Tisdag)]
    public void Calculate_ShouldReturnTisdag_When2000Century(int year, Weekday expected)
    {
        CenturyAnchorCalculator.Calculate(year).Should().Be(expected,
            $"Alla år på 2000-talet ska ha Tisdag som sekelankare");
    }

    [Theory]
    [InlineData(2100, Weekday.Söndag)]
    [InlineData(2150, Weekday.Söndag)]
    [InlineData(2199, Weekday.Söndag)]
    public void Calculate_ShouldReturnSöndag_When2100Century(int year, Weekday expected)
    {
        CenturyAnchorCalculator.Calculate(year).Should().Be(expected,
            $"Alla år på 2100-talet ska ha Söndag som sekelankare");
    }

    [Fact]
    public void GetAllAnchorsInCycle_ShouldReturnFourAnchors()
    {
        var anchors = CenturyAnchorCalculator.GetAllAnchorsInCycle();

        anchors.Should().HaveCount(4);
    }

    [Fact]
    public void GetInfo_ShouldReturnDescriptiveExplanation_WhenCalledForYear()
    {
        var info = CenturyAnchorCalculator.GetInfo(2024);

        info.CenturyStart.Should().Be(2000);
        info.AnchorDay.Should().Be(Weekday.Tisdag);
        info.Explanation.Should().NotBeNullOrWhiteSpace();
    }
}
