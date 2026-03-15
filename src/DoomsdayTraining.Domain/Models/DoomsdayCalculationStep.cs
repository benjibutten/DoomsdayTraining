namespace DoomsdayTraining.Domain.Models;

/// <summary>
/// Representerar ett enskilt steg i Doomsday-algoritmens beräkningsprocess.
/// Varje steg har en titel, en pedagogisk förklaring, och det beräknade resultatet.
/// Används för att visa steg-för-steg-lösningar i gränssnittet så att användaren
/// kan följa med i varje del av tankegången.
/// </summary>
public class DoomsdayCalculationStep
{
    /// <summary>
    /// Stegnummer (1, 2, 3...) för att visa ordningen i beräkningen.
    /// </summary>
    public int StepNumber { get; init; }

    /// <summary>
    /// Kort rubrik som beskriver vad steget handlar om.
    /// Exempel: "Bestäm sekelns ankare", "Beräkna årets Doomsday".
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// En pedagogisk förklaring av vad som händer i detta steg,
    /// skriven så att någon som aldrig sett algoritmen ska kunna förstå.
    /// </summary>
    public required string Explanation { get; init; }

    /// <summary>
    /// Den faktiska beräkningen uttryckt som en läsbar formel/sträng.
    /// Exempel: "24 ÷ 12 = 2 rest 0" eller "Sekeltal 20 → Tisdag".
    /// </summary>
    public required string Calculation { get; init; }

    /// <summary>
    /// Resultatet av detta steg, uttryckt som text.
    /// Exempel: "Tisdag" eller "4".
    /// </summary>
    public required string Result { get; init; }
}
