using DoomsdayTraining.Domain.Models;

namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Huvudklassen som orkesterar hela Doomsday-algoritmen och producerar
/// ett fullständigt steg-för-steg-resultat.
/// 
/// Doomsday-algoritmen (uppfunnen av John Horton Conway) gör det möjligt
/// att räkna ut vilken veckodag ett godtyckligt datum infaller på – i huvudet!
/// 
/// Algoritmen i fyra steg:
/// ═══════════════════════════════════════════════════════════════
/// STEG 1: Bestäm sekelankaret (Century Anchor)
///   → Varje sekel har en fast "ankardag". T.ex. 2000-talet = Tisdag.
///   
/// STEG 2: Beräkna årets Doomsday
///   → Justera sekelankaret baserat på de sista två siffrorna i årtalet
///     med "12-metoden" (dela med 12, ta rest, dela rest med 4, summera).
///   
/// STEG 3: Hitta månadens Doomsday-referensdatum
///   → Varje månad har kända datum som alltid infaller på årets Doomsday.
///     T.ex. 4/4, 6/6, 8/8, 10/10, 12/12 och "9-to-5 at 7-Eleven".
///   
/// STEG 4: Räkna avståndet till måldatumet
///   → Från Doomsday-referensen i månaden, räkna dagar till ditt datum.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class DoomsdayAlgorithm
{
    /// <summary>
    /// Beräknar veckodagen för ett givet datum med Doomsday-algoritmen
    /// och returnerar alla steg i beräkningen för pedagogisk visning.
    /// </summary>
    /// <param name="date">Datumet att beräkna veckodagen för.</param>
    /// <returns>
    /// Ett <see cref="DoomsdayResult"/> med slutresultatet och alla beräkningssteg.
    /// </returns>
    public static DoomsdayResult Calculate(DateOnly date)
    {
        var steps = new List<DoomsdayCalculationStep>();
        var stepNumber = 1;

        // ─── STEG 1: Skottårskontroll ───
        var isLeapYear = LeapYearChecker.IsLeapYear(date.Year);
        steps.Add(new DoomsdayCalculationStep
        {
            StepNumber = stepNumber++,
            Title = "Kontrollera skottår",
            Explanation = $"Är {date.Year} ett skottår? Vi kontrollerar: " +
                          $"delbart med 4? {(date.Year % 4 == 0 ? "Ja" : "Nej")}" +
                          (date.Year % 4 == 0 ? $", delbart med 100? {(date.Year % 100 == 0 ? "Ja" : "Nej")}" : "") +
                          (date.Year % 100 == 0 ? $", delbart med 400? {(date.Year % 400 == 0 ? "Ja" : "Nej")}" : "") +
                          ". Skottår påverkar januari och februari.",
            Calculation = $"{date.Year} → {(isLeapYear ? "Skottår" : "Inte skottår")}",
            Result = isLeapYear ? "Ja, det är skottår" : "Nej, inte skottår"
        });

        // ─── STEG 2: Sekelankare ───
        var centuryAnchor = CenturyAnchorCalculator.Calculate(date.Year);
        var centuryInfo = CenturyAnchorCalculator.GetInfo(date.Year);
        steps.Add(new DoomsdayCalculationStep
        {
            StepNumber = stepNumber++,
            Title = "Bestäm sekelankaret",
            Explanation = centuryInfo.Explanation +
                          " Minnesregel: FrOTS = Fredag (1800) → Onsdag (1900) → Tisdag (2000) → Söndag (2100).",
            Calculation = $"(5 × ({date.Year / 100} mod 4) + 2) mod 7 = {(int)centuryAnchor}",
            Result = $"{centuryAnchor}"
        });

        // ─── STEG 3: Årets Doomsday ───
        var (quotient, remainder, leapAdj, yearDoomsday) =
            YearDoomsdayCalculator.GetComponents(date.Year, centuryAnchor);
        var lastTwo = date.Year % 100;
        var yearTotal = (int)centuryAnchor + quotient + remainder + leapAdj;
        steps.Add(new DoomsdayCalculationStep
        {
            StepNumber = stepNumber++,
            Title = "Beräkna årets Doomsday (12-metoden)",
            Explanation = $"De sista två siffrorna: y = {lastTwo}.\n" +
                          $"  a) 12-årsblock: {lastTwo} ÷ 12 = {quotient} hela block → {quotient} hopp\n" +
                          $"  b) Överblivna år: {lastTwo} − {quotient * 12} = {remainder} år kvar\n" +
                          $"  c) Skottårshopp i resten: {remainder} ÷ 4 = {leapAdj} hela 4-årsgrupp(er) → {leapAdj} extra hopp\n" +
                          $"  d) Summera allt: sekelankare({(int)centuryAnchor}) + {quotient} + {remainder} + {leapAdj} = {yearTotal} → mod 7 = {(int)yearDoomsday}",
            Calculation = $"{(int)centuryAnchor} + {quotient} + {remainder} + {leapAdj} = {yearTotal} → mod 7 = {(int)yearDoomsday}",
            Result = $"{yearDoomsday}"
        });

        // ─── STEG 4: Månadens Doomsday-datum ───
        var doomsdayInMonth = MonthDoomsdayProvider.GetDoomsdayForMonth(date.Month, isLeapYear);
        var monthInfo = MonthDoomsdayProvider.GetInfoForMonth(date.Month);
        var doomsdayDate = new DateOnly(date.Year, date.Month, doomsdayInMonth);
        steps.Add(new DoomsdayCalculationStep
        {
            StepNumber = stepNumber++,
            Title = "Hitta månadens Doomsday-datum",
            Explanation = $"I {monthInfo.MonthName} är Doomsday-datumet den {doomsdayInMonth}:e. " +
                          $"Minnesregel: {monthInfo.Mnemonic} " +
                          $"Vi vet att {doomsdayInMonth}/{date.Month} = {yearDoomsday}.",
            Calculation = $"{date.Month}/{doomsdayInMonth} = {yearDoomsday}",
            Result = $"{doomsdayInMonth} {monthInfo.MonthName}"
        });

        // ─── STEG 5: Räkna till måldatumet ───
        var (diff, diffDescription) = WeekdayCalculator.GetDifferenceDescription(date.Day, doomsdayInMonth);
        var finalWeekday = WeekdayCalculator.Calculate(date.Day, doomsdayInMonth, yearDoomsday);
        steps.Add(new DoomsdayCalculationStep
        {
            StepNumber = stepNumber,
            Title = "Räkna fram till måldatumet",
            Explanation = diff == 0
                ? $"Datumet {date.Day}/{date.Month} ÄR Doomsday-referensdatumet! " +
                  $"Veckodagen är direkt {yearDoomsday}."
                : $"Vårt datum ({date.Day}/{date.Month}) ligger {diffDescription}.\n" +
                  $"Vi tar {yearDoomsday}({(int)yearDoomsday}) och " +
                  $"{(diff > 0 ? "lägger till" : "drar ifrån")} {Math.Abs(diff)}: " +
                  $"({(int)yearDoomsday} + {diff}) mod 7 = " +
                  $"{(((int)yearDoomsday + diff) % 7 + 7) % 7} → {finalWeekday}.",
            Calculation = diff == 0
                ? $"Doomsday-datum = måldatum → {yearDoomsday}"
                : $"({(int)yearDoomsday} + ({diff})) mod 7 = {(int)finalWeekday}",
            Result = $"{finalWeekday}"
        });

        return new DoomsdayResult
        {
            Date = date,
            Weekday = finalWeekday,
            IsLeapYear = isLeapYear,
            CenturyAnchor = centuryAnchor,
            YearDoomsday = yearDoomsday,
            MonthDoomsdayDate = doomsdayDate,
            Steps = steps
        };
    }

    /// <summary>
    /// Snabbmetod för att bara få veckodagen utan alla steg.
    /// Användbar för quiz-validering.
    /// </summary>
    /// <param name="date">Datumet att beräkna.</param>
    /// <returns>Veckodagen.</returns>
    public static Weekday GetWeekday(DateOnly date)
    {
        var isLeapYear = LeapYearChecker.IsLeapYear(date.Year);
        var centuryAnchor = CenturyAnchorCalculator.Calculate(date.Year);
        var yearDoomsday = YearDoomsdayCalculator.Calculate(date.Year, centuryAnchor);
        var doomsdayInMonth = MonthDoomsdayProvider.GetDoomsdayForMonth(date.Month, isLeapYear);

        return WeekdayCalculator.Calculate(date.Day, doomsdayInMonth, yearDoomsday);
    }
}
