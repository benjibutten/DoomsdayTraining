using DoomsdayTraining.Domain.Models;

namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Beräknar Doomsday-veckodagen för ett specifikt år.
/// 
/// Det här är STEG 2 i Doomsday-algoritmen.
/// 
/// När vi har sekelankaret (från steg 1) behöver vi justera det
/// för det specifika året. Vi använder de sista två siffrorna i årtalet.
/// 
/// Metod ("12-metoden" / Conway's metod):
/// 1. Ta de sista två siffrorna: y = år mod 100
/// 2. Dela y med 12:       kvot = y ÷ 12 (heltal), rest = y mod 12
/// 3. Dela resten med 4:   justering = rest ÷ 4 (heltal)
/// 4. Summera:              sekelankare + kvot + rest + justering
/// 5. Ta mod 7 för att få veckodagen.
/// 
/// Tanken bakom: 12 år = exakt 1 extra dag (efter att skottår räknats bort),
/// och var 4:e år i resten ger ytterligare en extra dag.
/// 
/// Exempel för år 2024:
///   y = 24
///   24 ÷ 12 = 2 (kvot), rest = 0
///   0 ÷ 4 = 0 (justering)
///   Tisdag(2) + 2 + 0 + 0 = 4 → Torsdag ✓
/// </summary>
public static class YearDoomsdayCalculator
{
    /// <summary>
    /// Beräknar årets Doomsday-veckodag givet sekelankaret.
    /// Denna metod visar exakt hur en människa räknar steg för steg.
    /// </summary>
    /// <param name="year">Hela årtalet (t.ex. 2024).</param>
    /// <param name="centuryAnchor">Sekelankardagen (från CenturyAnchorCalculator).</param>
    /// <returns>Veckodagen som alla Doomsday-datum infaller på detta år.</returns>
    public static Weekday Calculate(int year, Weekday centuryAnchor)
    {
        var (_, _, _, doomsday) = GetComponents(year, centuryAnchor);
        return doomsday;
    }

    /// <summary>
    /// Returnerar alla delkomponenter i beräkningen, för pedagogiskt bruk.
    /// </summary>
    /// <param name="year">Hela årtalet.</param>
    /// <param name="centuryAnchor">Sekelankaret.</param>
    /// <returns>
    /// En tuple med: kvoten (y÷12), resten (y mod 12), 
    /// justeringen (rest÷4), och slutresultatet (Weekday).
    /// </returns>
    public static (int Quotient, int Remainder, int LeapAdjustment, Weekday Doomsday) GetComponents(
        int year, Weekday centuryAnchor)
    {
        // Steg 1: Ta de sista två siffrorna
        var lastTwoDigits = year % 100;

        // Steg 2: Dela med 12
        var quotient = lastTwoDigits / 12;
        var remainder = lastTwoDigits % 12;

        // Steg 3: Dela resten med 4 (för att hantera skottår inom resten)
        var leapAdjustment = remainder / 4;

        // Steg 4: Summera allt och ta mod 7
        var total = (int)centuryAnchor + quotient + remainder + leapAdjustment;
        var doomsday = (Weekday)(total % 7);

        return (quotient, remainder, leapAdjustment, doomsday);
    }

    /// <summary>
    /// Beräknar årets Doomsday direkt från årtalet (hämtar sekelankaret automatiskt).
    /// Bekvämlighetsmetod som kombinerar steg 1 och 2.
    /// </summary>
    /// <param name="year">Hela årtalet.</param>
    /// <returns>Årets Doomsday-veckodag.</returns>
    public static Weekday Calculate(int year)
    {
        var centuryAnchor = CenturyAnchorCalculator.Calculate(year);
        return Calculate(year, centuryAnchor);
    }
}
