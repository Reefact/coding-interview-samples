#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace Slugger.Domain;

/// <summary>
///     The characters a slug ends with, drawn at random rather than taken from a theme. It is
///     neither a term nor a word: nothing in the vocabulary spells it.
/// </summary>
/// <remarks>
///     Which is why it has no way in but <see cref="TokenFactory" />. A term is read from a file
///     and so can be malformed; a token is produced, so the only thing that can be wrong about one
///     is the request that drew it - and that is the factory's business, not a refusal a report
///     would carry.
/// </remarks>
[ValueObject]
[DebuggerDisplay("{ToString()}")]
public sealed class Token : ValueType<Token> {

    #region Fields

    private readonly string _digits;

    #endregion

    #region Constructors & Destructor

    public Token(string digits) {
        _digits = digits;
    }

    #endregion

    /// <summary>How many characters the token carries, which a length budget counts like any other.</summary>
    public int Length => _digits.Length;

    /// <summary>The token, for a human reading a watch window.</summary>
    /// <remarks>A debugging aid, and not how the digits leave the type.</remarks>
    public override string ToString() {
        return _digits;
    }

    /// <summary>Two tokens spelled the same are the same token, whatever draw produced them.</summary>
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        yield return _digits;
    }

}
