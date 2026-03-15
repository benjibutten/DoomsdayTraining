namespace DoomsdayTraining.Domain.Models;

/// <summary>
/// Representerar veckodagarna med numeriska värden som matchar
/// Doomsday-algoritmens konvention (Söndag = 0, Måndag = 1, ... Lördag = 6).
/// Detta är samma numrering som används i beräkningarna genom hela algoritmen.
/// </summary>
public enum Weekday
{
    /// <summary>Söndag (0) – Veckans startdag i Doomsday-algoritmens numrering.</summary>
    Söndag = 0,

    /// <summary>Måndag (1)</summary>
    Måndag = 1,

    /// <summary>Tisdag (2)</summary>
    Tisdag = 2,

    /// <summary>Onsdag (3)</summary>
    Onsdag = 3,

    /// <summary>Torsdag (4)</summary>
    Torsdag = 4,

    /// <summary>Fredag (5)</summary>
    Fredag = 5,

    /// <summary>Lördag (6)</summary>
    Lördag = 6
}
