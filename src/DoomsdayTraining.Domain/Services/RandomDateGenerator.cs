namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Genererar slumpmässiga datum för övningsläget.
/// Kan anpassas efter svårighetsgrad – t.ex. bara datum inom 2000-talet
/// eller varierande sekel för att öva på alla sekelankare.
/// </summary>
public static class RandomDateGenerator
{
    private static readonly Random _random = new();

    /// <summary>
    /// Genererar ett slumpmässigt datum inom angivet år-intervall.
    /// Standardintervallet täcker 1800–2099 för att ge variation i sekelankare.
    /// </summary>
    /// <param name="minYear">Lägsta tillåtna år (standard: 1800).</param>
    /// <param name="maxYear">Högsta tillåtna år (standard: 2099).</param>
    /// <returns>Ett slumpmässigt datum.</returns>
    public static DateOnly Generate(int minYear = 1800, int maxYear = 2099)
    {
        var year = _random.Next(minYear, maxYear + 1);
        var month = _random.Next(1, 13);
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var day = _random.Next(1, daysInMonth + 1);

        return new DateOnly(year, month, day);
    }

    /// <summary>
    /// Genererar ett slumpmässigt datum inom ett specifikt sekel.
    /// Användbart för att öva på ett sekelankare i taget.
    /// </summary>
    /// <param name="centuryStart">Sekelstarten (t.ex. 1900 eller 2000).</param>
    /// <returns>Ett slumpmässigt datum inom det angivna seklet.</returns>
    public static DateOnly GenerateForCentury(int centuryStart)
    {
        return Generate(centuryStart, centuryStart + 99);
    }

    /// <summary>
    /// Genererar ett slumpmässigt datum inom en specifik månad (valfritt år-intervall).
    /// Användbart för att öva på Doomsday-datumen för en specifik månad.
    /// </summary>
    /// <param name="month">Månadsnummer (1–12).</param>
    /// <param name="minYear">Lägsta tillåtna år.</param>
    /// <param name="maxYear">Högsta tillåtna år.</param>
    /// <returns>Ett slumpmässigt datum i den angivna månaden.</returns>
    public static DateOnly GenerateForMonth(int month, int minYear = 1800, int maxYear = 2099)
    {
        var year = _random.Next(minYear, maxYear + 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var day = _random.Next(1, daysInMonth + 1);

        return new DateOnly(year, month, day);
    }
}
