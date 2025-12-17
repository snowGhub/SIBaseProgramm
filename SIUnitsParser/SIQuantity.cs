namespace SIUnitsParser;

public class SIQuantity
{
    public double ValueInBaseUnits { get; }
    public UnitDefinition Unit { get; }

    public SIQuantity(double valueInBaseUnits, UnitDefinition unit)
    {
        ValueInBaseUnits = valueInBaseUnits;
        Unit = unit;
    }
    
    public UnitType UnitType => Unit.Type;
    public string BaseUnitName => Unit.BaseName;
    public string BaseUnitSymbol => Unit.BaseSymbol;
}