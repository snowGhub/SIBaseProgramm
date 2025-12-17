using System.Text;

namespace SIUnitsParser;

public class PrefixRegistry
{
    private readonly List<Prefix> _prefixes;
    private readonly Dictionary<string, Prefix> _map;
    
    public IReadOnlyList<Prefix> Prefixes => _prefixes;

    private PrefixRegistry(List<Prefix> prefixes)
    {
        _prefixes = prefixes
            .OrderBy(p => p.Exponent)
            .ToList();
        
        var dict = new Dictionary<string, Prefix>(StringComparer.OrdinalIgnoreCase);
        foreach (var prefix in _prefixes)
        {
            dict[NormalizeToken(prefix.Symbol)] = prefix;
            dict[NormalizeToken(prefix.Name)] = prefix;

            if (prefix.Symbol == "µ")
                dict[NormalizeToken("u")] = prefix;

            if (prefix.Symbol == "u")
                dict[NormalizeToken("µ")] = prefix;
        }
        
        _map = dict;
    }

    public static PrefixRegistry CreateDefault()
    {
        static Prefix P(string symbol, string name, int exp)
            => new(symbol, name, exp, Math.Pow(10, exp));

        var list = new List<Prefix>
        {
            P("n", "nano", -9),
            P("µ", "micro", -6),
            P("m", "milli", -3),
            P("k", "kilo", 3),
            P("M", "mega", 6),
            P("G", "giga", 9),
        };

        return new PrefixRegistry(list);
    }

    public Prefix? TryGet(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;
        
        var key = NormalizeToken(token);
        return _map.GetValueOrDefault(key);
    }

    public Prefix ChooseAuto(double valueInBaseUnits)
    {
        var abs = Math.Abs(valueInBaseUnits);
        if (abs == 0) return new Prefix("", "", 0, 1);
        
        var candidates = _prefixes
            .Concat(new[] { new Prefix("", "", 0, 1) })
            .OrderBy(p => p.Exponent)
            .ToArray();

        Prefix best = new Prefix("", "", 0, 1);

        foreach (var candidate in candidates)
        {
            var scaled = abs / candidate.Factor;
            if (scaled >= 1 && scaled < 1000)
                best = candidate;
        }
        
        if (abs / candidates.First().Factor < 1) return candidates.First();
        if (abs / candidates.Last().Factor >= 1000) return candidates.Last();
        
        return best;
    }

    public static string NormalizeToken(string token)
    {
        var t = new string(token.Where(c => !char.IsWhiteSpace(c)).ToArray());
        t = t.Replace('µ', 'u');
        return t.ToLowerInvariant();
    }
}