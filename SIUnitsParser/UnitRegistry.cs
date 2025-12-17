namespace SIUnitsParser;

public class UnitRegistry
{
    private readonly PrefixRegistry _prefixes;
    private readonly IReadOnlyList<UnitDefinition> _units;
    private readonly IReadOnlyDictionary<string, UnitDefinition> _aliastoUnit;

    public UnitRegistry(PrefixRegistry prefixes, IEnumerable<UnitDefinition> units)
    {
        _prefixes = prefixes;
        _units = units.ToArray();

        var dict = new Dictionary<string, UnitDefinition>(StringComparer.OrdinalIgnoreCase);
        foreach (var u in _units)
        foreach (var a in u.Aliases)
        {
            dict[TokenNormalizer.Normalize(a)] = u;
        }

        _aliastoUnit = dict;
    }

    public static UnitRegistry CreateDefault(PrefixRegistry prefixes)
    {
        var units = new List<UnitDefinition>
        {
            new(
                "ohm",
                "Ω",
                UnitType.Resistance,
                true,
                new[] { "ohm", "Ω", "omega" }
            ),
            new(
                "volt",
                "V",
                UnitType.Voltage,
                true,
                new[] { "volt", "v" }
            ),
            new(
                "ampere",
                "A",
                UnitType.Current,
                true,
                new[] { "ampere", "amp", "a" }
            )
        };
        
        return new UnitRegistry(prefixes, units);
    }
}