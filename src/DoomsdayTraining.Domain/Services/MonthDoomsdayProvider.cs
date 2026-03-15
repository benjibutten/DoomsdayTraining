using DoomsdayTraining.Domain.Models;

namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Tillhandahåller de kända Doomsday-datumen för varje månad.
/// 
/// Det här är STEG 3 i Doomsday-algoritmen (kunskapsdelen).
/// 
/// Varje månad har minst ett datum som ALLTID infaller på årets Doomsday.
/// Genom att memorera dessa kan man snabbt hitta en referenspunkt
/// i vilken månad som helst och sedan räkna till måldatumet.
/// 
/// Minnesregler:
/// ─────────────────────────────────────────────────────
/// Jämna månader (april–december):    4/4, 6/6, 8/8, 10/10, 12/12
///   → "Jämn månad, jämnt datum – lika som månadsnumret!"
/// 
/// Udda månader:                      5/9, 9/5, 7/11, 11/7
///   → "Jag jobbar 9–5 på 7-Eleven" (I work 9-to-5 at 7-Eleven)
/// 
/// Januari och februari (specialfall):
///   Normalt år:  1/3 och 2/28
///   Skottår:     1/4 och 2/29
///   → "3:e januari normalt, 4:e vid skottår" (en extra dag)
///   → "Sista dagen i februari"
///
/// Mars: 7/3 (Pi-dagen 3/14 är också en Doomsday, liksom 3/7)
///   → "Sjunde mars" eller "Sista februari-dagen + 7"
/// ─────────────────────────────────────────────────────
/// </summary>
public static class MonthDoomsdayProvider
{
    /// <summary>
    /// Hämtar Doomsday-referensinformation för alla 12 månader.
    /// Innehåller datum, minnesregler och skottårsundantag.
    /// </summary>
    /// <returns>En lista med 12 <see cref="MonthDoomsdayInfo"/>-objekt.</returns>
    public static List<MonthDoomsdayInfo> GetAll()
    {
        return
        [
            new MonthDoomsdayInfo
            {
                Month = 1, MonthName = "Januari", Day = 3, LeapYearDay = 4,
                Mnemonic = "3 januari (eller 4 januari vid skottår). " +
                           "Tänk: skottår = en extra dag, så 3+1 = 4."
            },
            new MonthDoomsdayInfo
            {
                Month = 2, MonthName = "Februari", Day = 28, LeapYearDay = 29,
                Mnemonic = "Sista dagen i februari (28 eller 29 vid skottår). " +
                           "Den sista februari alltid doomsdagen!"
            },
            new MonthDoomsdayInfo
            {
                Month = 3, MonthName = "Mars", Day = 7,
                Mnemonic = "7 mars (även 14 mars, 21 mars, 28 mars). " +
                           "Multiplar av 7 i mars! 0 mars = sista feb = också Doomsday."
            },
            new MonthDoomsdayInfo
            {
                Month = 4, MonthName = "April", Day = 4,
                Mnemonic = "4/4 – April den fjärde! Jämn månad = samma tal."
            },
            new MonthDoomsdayInfo
            {
                Month = 5, MonthName = "Maj", Day = 9,
                Mnemonic = "9/5 – \"Jag jobbar 9 till 5\" (9-to-5)."
            },
            new MonthDoomsdayInfo
            {
                Month = 6, MonthName = "Juni", Day = 6,
                Mnemonic = "6/6 – Juni den sjätte! Jämn månad = samma tal."
            },
            new MonthDoomsdayInfo
            {
                Month = 7, MonthName = "Juli", Day = 11,
                Mnemonic = "7/11 – \"7-Eleven\"! Som minilivsbutiken."
            },
            new MonthDoomsdayInfo
            {
                Month = 8, MonthName = "Augusti", Day = 8,
                Mnemonic = "8/8 – Augusti den åttonde! Jämn månad = samma tal."
            },
            new MonthDoomsdayInfo
            {
                Month = 9, MonthName = "September", Day = 5,
                Mnemonic = "5/9 – \"Jag jobbar 9 till 5\" – spegelvänden! (5-9 bakvänt)."
            },
            new MonthDoomsdayInfo
            {
                Month = 10, MonthName = "Oktober", Day = 10,
                Mnemonic = "10/10 – Oktober den tionde! Jämn månad = samma tal."
            },
            new MonthDoomsdayInfo
            {
                Month = 11, MonthName = "November", Day = 7,
                Mnemonic = "11/7 – \"7-Eleven\" bakvänt! (7-11 → 11/7)."
            },
            new MonthDoomsdayInfo
            {
                Month = 12, MonthName = "December", Day = 12,
                Mnemonic = "12/12 – December den tolfte! Jämn månad = samma tal."
            }
        ];
    }

    /// <summary>
    /// Hämtar Doomsday-referensdatumet för en specifik månad.
    /// Tar hänsyn till om det är skottår (viktigt för januari och februari).
    /// </summary>
    /// <param name="month">Månadsnummer (1–12).</param>
    /// <param name="isLeapYear">Om det aktuella året är ett skottår.</param>
    /// <returns>Doomsday-referensdagen i månaden.</returns>
    public static int GetDoomsdayForMonth(int month, bool isLeapYear)
    {
        var info = GetAll().First(m => m.Month == month);

        // Januari och februari har speciella skottårsdatum
        if (isLeapYear && info.LeapYearDay.HasValue)
            return info.LeapYearDay.Value;

        return info.Day;
    }

    /// <summary>
    /// Hämtar information för en specifik månad.
    /// </summary>
    /// <param name="month">Månadsnummer (1–12).</param>
    /// <returns>Doomsday-information för månaden.</returns>
    public static MonthDoomsdayInfo GetInfoForMonth(int month)
    {
        return GetAll().First(m => m.Month == month);
    }
}
