namespace DoomsdayTraining.Domain.Services;

/// <summary>
/// Kontrollerar om ett år är ett skottår.
/// 
/// Skottårsreglerna (som man måste kunna för Doomsday-algoritmen):
/// 1. Om året är delbart med 4 → det ÄR (troligtvis) ett skottår
/// 2. MEN om det också är delbart med 100 → det är INTE ett skottår
/// 3. MEN MEN om det dessutom är delbart med 400 → det ÄR ett skottår ändå!
/// 
/// Exempel:
/// - 2024: delbart med 4, inte med 100 → skottår ✓
/// - 1900: delbart med 4 och 100, men inte 400 → INTE skottår ✗
/// - 2000: delbart med 4, 100 OCH 400 → skottår ✓
/// </summary>
public static class LeapYearChecker
{
    /// <summary>
    /// Avgör om det angivna året är ett skottår enligt de tre reglerna:
    /// delbart med 4, men inte 100, om inte också 400.
    /// </summary>
    /// <param name="year">Året att kontrollera.</param>
    /// <returns>True om det är ett skottår, annars false.</returns>
    public static bool IsLeapYear(int year)
    {
        // Regel 3: Delbart med 400 → alltid skottår
        if (year % 400 == 0)
            return true;

        // Regel 2: Delbart med 100 → inte skottår
        if (year % 100 == 0)
            return false;

        // Regel 1: Delbart med 4 → skottår
        return year % 4 == 0;
    }
}
