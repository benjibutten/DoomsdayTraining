namespace DoomsdayTraining.Domain.Models;

/// <summary>
/// Representerar resultatet av en övningsfråga i quiz-/flashcard-läget.
/// Sparar vad användaren gissade, vad rätt svar var, och hur lång tid det tog.
/// </summary>
public class PracticeResult
{
    /// <summary>
    /// Datumet som användaren skulle räkna ut veckodagen för.
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Användarens gissning.
    /// </summary>
    public Weekday UserAnswer { get; init; }

    /// <summary>
    /// Det korrekta svaret.
    /// </summary>
    public Weekday CorrectAnswer { get; init; }

    /// <summary>
    /// Var svaret korrekt?
    /// </summary>
    public bool IsCorrect => UserAnswer == CorrectAnswer;

    /// <summary>
    /// Hur lång tid det tog för användaren att svara (i sekunder).
    /// </summary>
    public double TimeTakenSeconds { get; init; }
}
