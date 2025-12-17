namespace SIUnitsParser;

public sealed record UnitDefinition(
    string BaseName,
    string BaseSymbol,
    UnitType Type,
    bool AllowPrefixes,
    IReadOnlyList<string> Aliases
    );