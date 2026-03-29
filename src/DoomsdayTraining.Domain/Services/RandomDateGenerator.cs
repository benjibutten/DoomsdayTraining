namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Genererar slumpmässiga datum för övningsläget.
/// Kan anpassas efter svårighetsgrad – t.ex. bara datum inom 2000-talet
/// eller varierande sekel för att öva på alla sekelankare.
/// </summary>
public static class RandomDateGenerator
{
    private static readonly Random _random = new();

    public const int DefaultMinYear = 1800;
    public const int DefaultMaxYear = 2099;
    public const int AllCenturiesMinYear = 1800;
    public const int AllCenturiesMaxYear = 2199;
    public const int ExtendedPracticeMinYear = 1600;
    public const int ExtendedPracticeMaxYear = 2399;

    private const int FirstMonth = 1;
    private const int LastMonth = 12;
    private const int FirstDayInMonth = 1;

    /// <summary>
    /// Genererar ett slumpmässigt datum inom angivet år-intervall.
    /// Standardintervallet täcker 1800–2099 för att ge variation i sekelankare.
    /// </summary>
    /// <param name="minYear">Lägsta tillåtna år (standard: <see cref="DefaultMinYear"/>).</param>
    /// <param name="maxYear">Högsta tillåtna år (standard: <see cref="DefaultMaxYear"/>).</param>
    /// <returns>Ett slumpmässigt datum.</returns>
    public static DateOnly Generate(int minYear = DefaultMinYear, int maxYear = DefaultMaxYear)
    {
        var year = _random.Next(minYear, maxYear + 1);
        var month = _random.Next(FirstMonth, LastMonth + 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var day = _random.Next(FirstDayInMonth, daysInMonth + 1);

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
    public static DateOnly GenerateForMonth(int month, int minYear = DefaultMinYear, int maxYear = DefaultMaxYear)
    {
        var year = _random.Next(minYear, maxYear + 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var day = _random.Next(FirstDayInMonth, daysInMonth + 1);

        return new DateOnly(year, month, day);
    }
}
