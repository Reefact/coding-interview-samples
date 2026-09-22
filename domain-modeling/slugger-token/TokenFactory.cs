#region Usings declarations

using System.Text;

#endregion

namespace Slugger.Domain;

/// <summary>
///     Draws a <see cref="Token" />, or decides that this slug carries none.
/// </summary>
/// <remarks>
///     The chance is part of the draw rather than a filter around it, because a token that shows up
///     rarely is the point: it reproduces docker's collision suffix without implementing collision
///     detection. How many draws it takes from the source is load-bearing for a scripted test, so
///     it is fixed here - a certainty and an impossibility both skip the roll, and everything
///     between rolls once before the digits.
/// </remarks>
public static class TokenFactory {

    private const string DecimalDigits     = "0123456789";
    private const string HexadecimalDigits = "0123456789abcdef";

    #region Static members

    /// <summary>Draws a token of that many characters, or returns null when the chance says not to.</summary>
    /// <param name="random">Where the draw comes from.</param>
    /// <param name="length">How many characters to draw. At least one - a token of none is no token.</param>
    /// <param name="alphabet">What to draw them from.</param>
    /// <param name="chance">How often a token appears at all, from 0 to 100.</param>
    /// <exception cref="ArgumentOutOfRangeException">The length is below one, or the chance is outside 0 to 100.</exception>
    public static Token? Draw(IRandomSource random, int length, TokenAlphabet alphabet, int chance) {
        ArgumentNullException.ThrowIfNull(random);
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);
        ArgumentOutOfRangeException.ThrowIfNegative(chance);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(chance, 100);

        if (chance == 0) { return null; }

        // Next(100) lands in 0..99, so a chance of 100 always draws and one of 1 draws a hundredth
        // of the time. Neither end rolls: a certainty and an impossibility have nothing to decide,
        // and a roll they do not need would shift every draw a scripted test wrote down after it.
        if (chance < 100 && random.Next(100) >= chance) { return null; }

        string        digits = DigitsOf(alphabet);
        StringBuilder drawn  = new(length);
        for (int position = 0; position < length; position++) {
            drawn.Append(digits[random.Next(digits.Length)]);
        }

        return new Token(drawn.ToString());
    }

    /// <summary>A token that always appears, for a caller that has no chance to apply.</summary>
    /// <param name="random">Where the draw comes from.</param>
    /// <param name="length">How many characters to draw.</param>
    /// <param name="alphabet">What to draw them from.</param>
    public static Token Draw(IRandomSource random, int length, TokenAlphabet alphabet) {
        return Draw(random, length, alphabet, 100)!;
    }

    private static string DigitsOf(TokenAlphabet alphabet) {
        return alphabet == TokenAlphabet.Hexadecimal ? HexadecimalDigits : DecimalDigits;
    }

    #endregion

}
