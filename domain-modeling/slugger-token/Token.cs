#region Usings declarations

using System.Diagnostics;
using System.Text;

using Value;

#endregion

namespace Slugger.Domain;

/// <summary>
///     The characters a slug ends with, drawn at random rather than taken from a theme. It is
///     neither a term nor a word: nothing in the vocabulary spells it.
/// </summary>
/// <remarks>
///     A token is always valid: it carries at least one character, and every one of them belongs
///     to the alphabet it was drawn from. <see cref="Draw" /> is the only way in, so the type holds
///     that on its own rather than trusting whoever built it.
/// </remarks>
[ValueObject]
[DebuggerDisplay("{ToString()}")]
public sealed class Token : ValueType<Token> {

    #region Static members

    /// <summary>Draws a token of that many characters from the alphabet.</summary>
    /// <param name="alphabet">What to draw them from.</param>
    /// <param name="length">How many characters to draw. At least one - a token of none is no token.</param>
    /// <param name="random">Where the draw comes from.</param>
    /// <exception cref="ArgumentOutOfRangeException">The length is below one.</exception>
    /// <remarks>Takes exactly <paramref name="length" /> values from the source.</remarks>
    public static Token Draw(TokenAlphabet alphabet, int length, IRandomSource random) {
        ArgumentNullException.ThrowIfNull(alphabet);
        ArgumentNullException.ThrowIfNull(random);
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);

        StringBuilder drawn = new(length);
        for (int position = 0; position < length; position++) {
            drawn.Append(alphabet.Draw(random));
        }

        return new Token(drawn.ToString());
    }

    #endregion

    #region Fields

    private readonly string _characters;

    #endregion

    #region Constructors & Destructor

    private Token(string characters) {
        _characters = characters;
    }

    #endregion

    /// <summary>How many characters the token carries, which a length budget counts like any other.</summary>
    public int Length => _characters.Length;

    /// <summary>The token, for a human reading a watch window.</summary>
    /// <remarks>A debugging aid, and not how the characters leave the type.</remarks>
    public override string ToString() {
        return _characters;
    }

    /// <summary>Two tokens spelled the same are the same token, whatever draw produced them.</summary>
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        yield return _characters;
    }

}
