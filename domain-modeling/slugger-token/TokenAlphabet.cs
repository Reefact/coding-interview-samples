#region Usings declarations

using System.Diagnostics;

#endregion

namespace Slugger.Domain;

/// <summary>The characters a token is drawn from.</summary>
/// <remarks>
///     A closed set: the only alphabets are the ones declared here, and each one is a single
///     instance, so two alphabets are equal exactly when they are the same alphabet.
/// </remarks>
[DebuggerDisplay("{_characters}")]
public sealed class TokenAlphabet {

    #region Static members

    /// <summary>The ten decimal digits, which is what docker's collision suffix uses.</summary>
    public static readonly TokenAlphabet Decimal = new("0123456789");

    /// <summary>The same ten and the six letters above them, lowercase.</summary>
    public static readonly TokenAlphabet Hexadecimal = new("0123456789abcdef");

    #endregion

    #region Fields

    private readonly string _characters;

    #endregion

    #region Constructors & Destructor

    private TokenAlphabet(string characters) {
        _characters = characters;
    }

    #endregion

    /// <summary>Draws one character of the alphabet, every character being equally likely.</summary>
    /// <param name="random">Where the draw comes from.</param>
    /// <remarks>Takes exactly one value from the source, which a scripted test relies on.</remarks>
    public char Draw(IRandomSource random) {
        ArgumentNullException.ThrowIfNull(random);

        return _characters[random.Next(_characters.Length)];
    }

}
