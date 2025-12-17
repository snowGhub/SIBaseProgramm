namespace SIUnitsParser;

internal static class TokenNormalizer
{
    public static string Normalize(string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return string.Empty;
        
        var compact = new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
        compact = compact.Replace('µ', 'u');
        return compact.ToLowerInvariant();
    }
}