namespace DoomsdayTraining.Domain.Models;

/// <summary>
/// Information om ett känt Doomsday-datum för en specifik månad.
/// Doomsday-datum är datum som alltid infaller på årets Doomsday-veckodag.
/// Genom att memorera dessa kan man snabbt räkna ut vilken veckodag
/// vilket datum som helst infaller på.
/// </summary>
public class MonthDoomsdayInfo
{
    /// <summary>
    /// Månadsnummer (1–12).
    /// </summary>
    public int Month { get; init; }

    /// <summary>
    /// Månadens namn på svenska.
    /// </summary>
    public required string MonthName { get; init; }

    /// <summary>
    /// Dagen i månaden som är ett känt Doomsday-datum.
    /// Exempel: April → 4, Juni → 6, Juli → 11.
    /// </summary>
    public int Day { get; init; }

    /// <summary>
    /// Alternativ dag för skottår (gäller bara januari och februari).
    /// Januari: 3 normalt, 4 vid skottår.
    /// Februari: 28 normalt, 29 vid skottår.
    /// Null för övriga månader.
    /// </summary>
    public int? LeapYearDay { get; init; }

    /// <summary>
    /// En minnesregel eller förklaring som hjälper användaren
    /// att komma ihåg just detta Doomsday-datum.
    /// </summary>
    public required string Mnemonic { get; init; }
}
