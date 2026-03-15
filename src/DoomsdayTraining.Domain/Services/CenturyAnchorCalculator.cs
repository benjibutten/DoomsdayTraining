using DoomsdayTraining.Domain.Models;

namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Beräknar sekelns ankardag (Century Anchor).
/// 
/// Det här är STEG 1 i Doomsday-algoritmen.
/// 
/// Varje sekel har en fast "ankardag" – en veckodag som utgör basen
/// för alla beräkningar inom det seklet. Cykeln upprepar sig var 400:e år:
/// 
///   1800-talet → Fredag (5)
///   1900-talet → Onsdag (3)
///   2000-talet → Tisdag (2)
///   2100-talet → Söndag (0)
///   
/// Formeln: ankardag = (5 × (sekeltal mod 4) + 2) mod 7
/// 
/// Där "sekeltal" = de första två siffrorna i årtalet (t.ex. 20 för 2024).
/// 
/// Tips för att memorera: "FrOTS" = Fredag, Onsdag, Tisdag, Söndag.
/// </summary>
public static class CenturyAnchorCalculator
{
    /// <summary>
    /// De fyra sekelankarna som cykliskt upprepas (startår → ankardag).
    /// Cykeln börjar vid sekel vars nummer mod 4 = 2 (dvs. 1800, 2200, ...).
    /// </summary>
    private static readonly Dictionary<int, Weekday> _knownAnchors = new()
    {
        { 18, Weekday.Fredag },
        { 19, Weekday.Onsdag },
        { 20, Weekday.Tisdag },
        { 21, Weekday.Söndag }
    };

    /// <summary>
    /// Beräknar sekelns ankardag med formeln:
    /// ankardag = (5 × (sekeltal mod 4) + 2) mod 7
    /// </summary>
    /// <param name="year">Vilket år som helst (t.ex. 2024, 1987, 1776).</param>
    /// <returns>Veckodagen som är sekelankare för det aktuella året.</returns>
    public static Weekday Calculate(int year)
    {
        // Extrahera sekeltalet (t.ex. 2024 → 20, 1987 → 19)
        var centuryNumber = year / 100;

        // Formel: (5 × (sekeltal mod 4) + 2) mod 7
        var anchorValue = (5 * (centuryNumber % 4) + 2) % 7;

        return (Weekday)anchorValue;
    }

    /// <summary>
    /// Returnerar pedagogisk information om sekelankaret för ett givet år,
    /// inklusive förklaring av hur beräkningen går till.
    /// </summary>
    /// <param name="year">Året att hämta sekelankare-info för.</param>
    /// <returns>Ett <see cref="CenturyAnchorInfo"/>-objekt med detaljer.</returns>
    public static CenturyAnchorInfo GetInfo(int year)
    {
        var centuryNumber = year / 100;
        var anchor = Calculate(year);
        var centuryStart = centuryNumber * 100;

        return new CenturyAnchorInfo
        {
            CenturyStart = centuryStart,
            AnchorDay = anchor,
            Explanation = $"Sekeltalet är {centuryNumber}. " +
                          $"Formeln: (5 × ({centuryNumber} mod 4) + 2) mod 7 = " +
                          $"(5 × {centuryNumber % 4} + 2) mod 7 = " +
                          $"{5 * (centuryNumber % 4) + 2} mod 7 = {(int)anchor}. " +
                          $"Sekelankaret för {centuryStart}-talet är alltså {anchor}."
        };
    }

    /// <summary>
    /// Returnerar information om alla fyra sekelankarna i 400-årscykeln.
    /// Användbart för lärandesektionen så att man kan se mönstret.
    /// </summary>
    /// <returns>En lista med de fyra sekelankarna.</returns>
    public static List<CenturyAnchorInfo> GetAllAnchorsInCycle()
    {
        return _knownAnchors.Select(kvp => new CenturyAnchorInfo
        {
            CenturyStart = kvp.Key * 100,
            AnchorDay = kvp.Value,
            Explanation = $"{kvp.Key * 100}-talet har ankardagen {kvp.Value}."
        }).ToList();
    }
}
