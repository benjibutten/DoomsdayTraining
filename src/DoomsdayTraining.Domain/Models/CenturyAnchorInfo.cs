namespace DoomsdayTraining.Domain.Models;

/// <summary>
/// Information om en sekels ankardag inklusive pedagogisk förklaring.
/// Seklets ankare är startpunkten i Doomsday-algoritmen – det är den
/// veckodag som alla Doomsday-datum faller på för ett "jämnt sekelår"
/// (t.ex. 1900, 2000, 2100).
/// </summary>
public class CenturyAnchorInfo
{
    /// <summary>
    /// Sekeltalets startvärde (t.ex. 1800, 1900, 2000, 2100).
    /// </summary>
    public int CenturyStart { get; init; }

    /// <summary>
    /// Ankardagen (veckodagen) för detta sekel.
    /// </summary>
    public Weekday AnchorDay { get; init; }

    /// <summary>
    /// Pedagogisk förklaring av hur ankardagen beräknas.
    /// </summary>
    public required string Explanation { get; init; }
}
