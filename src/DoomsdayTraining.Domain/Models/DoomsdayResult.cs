namespace DoomsdayTraining.Domain.Models;

/// <summary>
/// Innehåller det fullständiga resultatet av en Doomsday-beräkning,
/// inklusive alla mellansteg. Detta gör det möjligt att visa hela
/// tankegången from sekelankare till slutgiltig veckodag.
/// </summary>
public class DoomsdayResult
{
    /// <summary>
    /// Datumet som beräknades.
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Den slutgiltiga veckodagen som algoritmen beräknade.
    /// </summary>
    public Weekday Weekday { get; init; }

    /// <summary>
    /// Om året är ett skottår. Viktigt eftersom januari och februari
    /// har olika Doomsday-datum beroende på om det är skottår.
    /// </summary>
    public bool IsLeapYear { get; init; }

    /// <summary>
    /// Sekelns ankardag (Century Anchor).
    /// Exempel: 2000-talet har Tisdag som ankare.
    /// </summary>
    public Weekday CenturyAnchor { get; init; }

    /// <summary>
    /// Årets Doomsday – den veckodag som alla kända Doomsday-datum
    /// faller på under det specifika året.
    /// </summary>
    public Weekday YearDoomsday { get; init; }

    /// <summary>
    /// Det Doomsday-referensdatum i samma månad som användes
    /// för den slutgiltiga beräkningen (t.ex. 4/4, 6/6, 7/11 osv.).
    /// </summary>
    public DateOnly MonthDoomsdayDate { get; init; }

    /// <summary>
    /// Alla beräkningssteg, i ordning, så att användaren kan följa
    /// med i hela tankegången från start till mål.
    /// </summary>
    public List<DoomsdayCalculationStep> Steps { get; init; } = [];
}
