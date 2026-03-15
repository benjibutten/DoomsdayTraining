using DoomsdayTraining.Domain.Models;

namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Beräknar slutgiltiga veckodagen från ett Doomsday-referensdatum.
/// 
/// Det här är STEG 4 (sista steget) i Doomsday-algoritmen.
/// 
/// Nu vet vi:
///   - Vilken veckodag som Doomsday-datumen infaller på (från steg 1+2)
///   - Vilket Doomsday-datum som ligger närmast vårt måldatum (från steg 3)
/// 
/// Allt vi behöver göra nu är att räkna skillnaden i dagar mellan
/// Doomsday-referensdatumet och vårt måldatum, och lägga till den
/// skillnaden till Doomsday-veckodagen.
/// 
/// Exempel: Om Doomsday = Torsdag och vi vill veta 10 april:
///   Referens: 4 april (Torsdag)
///   Skillnad: 10 - 4 = 6 dagar framåt
///   Torsdag + 6 = Onsdag ✓
/// </summary>
public static class WeekdayCalculator
{
    /// <summary>
    /// Beräknar veckodagen för ett godtyckligt datum baserat på
    /// årets Doomsday och Doomsday-referensdatumet för månaden.
    /// </summary>
    /// <param name="targetDay">Dagen i månaden vi vill räkna ut veckodagen för.</param>
    /// <param name="doomsdayInMonth">Doomsday-referensdagen i samma månad.</param>
    /// <param name="yearDoomsday">Årets Doomsday-veckodag.</param>
    /// <returns>Veckodagen för måldatumet.</returns>
    public static Weekday Calculate(int targetDay, int doomsdayInMonth, Weekday yearDoomsday)
    {
        // Räkna skillnaden i dagar (kan vara negativt om vi räknar bakåt)
        var dayDifference = targetDay - doomsdayInMonth;

        // Lägg till skillnaden till Doomsday-veckodagen och normalisera med mod 7
        // Vi lägger till 7 innan mod för att säkerställa positivt resultat vid negativ skillnad
        var weekdayValue = (((int)yearDoomsday + dayDifference) % 7 + 7) % 7;

        return (Weekday)weekdayValue;
    }

    /// <summary>
    /// Returnerar antal dagar skillnad och riktning, för pedagogisk visning.
    /// </summary>
    /// <param name="targetDay">Måldagen.</param>
    /// <param name="doomsdayInMonth">Doomsday-referensdagen i månaden.</param>
    /// <returns>
    /// Skillnaden med kontextuell text (t.ex. "6 dagar framåt" eller "2 dagar bakåt").
    /// </returns>
    public static (int Difference, string Description) GetDifferenceDescription(
        int targetDay, int doomsdayInMonth)
    {
        var diff = targetDay - doomsdayInMonth;

        var description = diff switch
        {
            0 => "Exakt på Doomsday-datumet – ingen räkning behövs!",
            > 0 => $"{diff} {(diff == 1 ? "dag" : "dagar")} framåt från Doomsday-datumet",
            < 0 => $"{-diff} {(-diff == 1 ? "dag" : "dagar")} bakåt från Doomsday-datumet"
        };

        return (diff, description);
    }
}
