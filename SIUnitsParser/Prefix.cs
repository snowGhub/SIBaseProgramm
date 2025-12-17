namespace SIUnitsParser;

public sealed record Prefix(
    string Symbol,
    string Name,
    int Exponent,
    double Factor
    );