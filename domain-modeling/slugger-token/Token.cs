#region Usings declarations

using System.Diagnostics;

using Value;

#endregion

namespace Slugger.Domain;

/// <summary>
///     Les caractères qui terminent un slug, tirés au hasard plutôt que pris dans un thème. Ce
///     n'est ni un terme ni un mot : rien dans le vocabulaire ne l'écrit.
/// </summary>
/// <remarks>
///     C'est pourquoi on ne peut l'obtenir que par <see cref="TokenFactory" />. Un terme est lu
///     dans un fichier, il peut donc être mal formé ; un token, lui, est produit : la seule chose
///     qui puisse y être fausse, c'est la demande qui l'a tiré. Et cela regarde la factory : ce
///     n'est pas un rejet qu'un rapport aurait à signaler.
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

    /// <summary>Le nombre de caractères du token, qu'un budget de longueur compte comme les autres.</summary>
    public int Length => _digits.Length;

    /// <summary>Le token, pour qui l'inspecte dans la fenêtre Espion du débogueur.</summary>
    /// <remarks>Une aide au débogage, et non la façon dont les chiffres sortent du type.</remarks>
    public override string ToString() {
        return _digits;
    }

    /// <summary>Deux tokens qui s'écrivent pareil sont le même token, quel que soit le tirage qui les a produits.</summary>
    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
        yield return _digits;
    }

}
