using DoomsdayTraining.Domain.Services;

namespace DoomsdayTraining.Domain.Tests;

/// <summary>
/// Tester för slumpmässig datumgenerering.
/// Verifierar att genererade datum ligger inom angivna intervall.
/// </summary>
public class RandomDateGeneratorTests
{
    [Fact]
    public void Generate_ShouldReturnDateWithinDefaultRange()
    {
        for (int i = 0; i < 100; i++)
        {
            var date = RandomDateGenerator.Generate();

            date.Year.Should().BeInRange(1800, 2099);
            date.Month.Should().BeInRange(1, 12);
            date.Day.Should().BeGreaterThan(0);
        }
    }

    [Fact]
    public void Generate_ShouldRespectCustomRange_WhenMinMaxProvided()
    {
        for (int i = 0; i < 100; i++)
        {
            var date = RandomDateGenerator.Generate(2000, 2000);

            date.Year.Should().Be(2000);
        }
    }

    [Fact]
    public void GenerateForCentury_ShouldReturnDateInCorrectCentury()
    {
        for (int i = 0; i < 100; i++)
        {
            var date = RandomDateGenerator.GenerateForCentury(1900);

            date.Year.Should().BeInRange(1900, 1999);
        }
    }

    [Fact]
    public void GenerateForMonth_ShouldReturnCorrectMonth_WhenMonthSpecified()
    {
        for (int i = 0; i < 100; i++)
        {
            var date = RandomDateGenerator.GenerateForMonth(6);

            date.Month.Should().Be(6);
        }
    }
}
