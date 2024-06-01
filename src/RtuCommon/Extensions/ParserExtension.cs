using RtuCommon.Models.Types;

namespace RtuCommon.Extensions;

public static class ParserExtension
{
    public static DinType ParseDinType(this byte[] bytes)
    {
        if (bytes.Length != 1)
        {
            throw new ArgumentException("DinType must be 1 byte");
        }
        
        return (DinType) bytes[0];
    }
    
    public static DisarmArmType ParseDisarmArmType(this byte[] bytes)
    {
        if (bytes.Length != 1)
        {
            throw new ArgumentException("DisarmArmType must be 1 byte");
        }
        
        return (DisarmArmType) bytes[0];
    }
    
    public static ControlAuthorization ParseControlAuthorization(this byte[] bytes)
    {
        if (bytes.Length != 1)
        {
            throw new ArgumentException("ControlAuthorization must be 1 byte");
        }
        
        return (ControlAuthorization) bytes[0];
    }
    
    public static RelaySwitchType ParseRelaySwitchType(this byte[] bytes)
    {
        if (bytes.Length != 1)
        {
            throw new ArgumentException("ControlType must be 1 byte");
        }
        
        return (RelaySwitchType) bytes[0];
    }
}