namespace Slugger.Domain;

/// <summary>Les caractères parmi lesquels un token est tiré.</summary>
public enum TokenAlphabet {

    /// <summary>Les dix chiffres décimaux, ceux qu'utilise le suffixe de collision de Docker.</summary>
    Decimal,

    /// <summary>Les dix mêmes, suivis des six premières lettres, en minuscules.</summary>
    Hexadecimal

}
