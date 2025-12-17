namespace SIUnitsParser.exceptions;

public class UnknownUnitException : SiParseException
{
    public UnknownUnitException(string token) : base($"Unknown unit: '{token}'.")
    { }
}