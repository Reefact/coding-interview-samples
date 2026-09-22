namespace Slugger.Domain;

/// <summary>The characters a token is drawn from.</summary>
public enum TokenAlphabet {

    /// <summary>The ten decimal digits, which is what docker's collision suffix uses.</summary>
    Decimal,

    /// <summary>The same ten and the six letters above them, lowercase.</summary>
    Hexadecimal

}
