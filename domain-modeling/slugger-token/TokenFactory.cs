#region Usings declarations

using System.Text;

#endregion

namespace Slugger.Domain;

/// <summary>
///     Tire un <see cref="Token" />, ou décide que ce slug n'en porte pas.
/// </summary>
/// <remarks>
///     La probabilité fait partie du tirage au lieu d'être un filtre autour, parce que tout
///     l'intérêt est qu'un token apparaisse rarement : on reproduit ainsi le suffixe de collision
///     de Docker sans implémenter de détection de collision. Le nombre de valeurs prises dans la
///     source compte pour les tests scriptés, il est donc fixé ici : une certitude comme une
///     impossibilité se passent du lancer, et tout ce qui est entre les deux lance une fois avant
///     de tirer les chiffres.
/// </remarks>
public static class TokenFactory {

    private const string DecimalDigits     = "0123456789";
    private const string HexadecimalDigits = "0123456789abcdef";

    #region Static members

    /// <summary>Tire un token de ce nombre de caractères, ou renvoie null quand la probabilité dit non.</summary>
    /// <param name="alphabet">L'alphabet dans lequel les tirer.</param>
    /// <param name="length">Le nombre de caractères à tirer. Au moins un : un token vide n'est pas un token.</param>
    /// <param name="random">La source du tirage.</param>
    public static Token? Draw(TokenAlphabet alphabet, int length, IRandomSource random) {
        string        digits = DigitsOf(alphabet);
        StringBuilder drawn  = new(length);
        for (int position = 0; position < length; position++) {
            drawn.Append(digits[random.Next(digits.Length)]);
        }

        return new Token(drawn.ToString());
    }

    /// <summary>Un token qui apparaît à coup sûr, pour l'appelant qui n'a pas de probabilité à appliquer.</summary>
    /// <param name="random">La source du tirage.</param>
    /// <param name="length">Le nombre de caractères à tirer.</param>
    /// <param name="alphabet">L'alphabet dans lequel les tirer.</param>
    public static Token Draw(IRandomSource random, int length, TokenAlphabet alphabet) {
        return Draw(random, length, alphabet, 100)!;
    }

    private static string DigitsOf(TokenAlphabet alphabet) {
        return alphabet == TokenAlphabet.Hexadecimal ? HexadecimalDigits : DecimalDigits;
    }

    #endregion

}
