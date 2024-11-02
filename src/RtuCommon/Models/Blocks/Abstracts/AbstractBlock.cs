using System.Globalization;
using RtuCommon.Extensions;
using RtuCommon.Interfaces;

namespace RtuCommon.Models.Blocks.Abstracts;

public abstract class AbstractBlock : IBlock
{
    private static readonly Dictionary<string, string> Shift0 = new()
    {
        { "00", "83" },
        { "01", "82" },
        { "02", "81" },
        { "03", "80" },
        { "04", "87" },
        { "05", "86" },
        { "06", "85" },
        { "07", "84" },
        { "08", "8b" },
        { "09", "8a" },
        { "0a", "89" },
        { "0b", "88" },
        { "0c", "8f" },
        { "0d", "8e" },
        { "0e", "8d" },
        { "0f", "8c" },
    };

    private static readonly Dictionary<string, string> Shift1 = new()
    {
        { "00", "82" },
        { "01", "83" },
        { "02", "80" },
        { "03", "81" },
        { "04", "86" },
        { "05", "87" },
        { "06", "84" },
        { "07", "85" },
        { "08", "8a" },
        { "09", "8b" },
        { "0a", "88" },
        { "0b", "89" },
        { "0c", "8e" },
        { "0d", "8f" },
        { "0e", "8c" },
        { "0f", "8d" },
    };

    private static readonly Dictionary<string, string> Shift3 = new()
    {
        { "00", "80" },
        { "01", "81" },
        { "02", "82" },
        { "03", "83" },
        { "04", "84" },
        { "05", "85" },
        { "06", "86" },
        { "07", "87" },
        { "08", "88" },
        { "09", "89" },
        { "0a", "8a" },
        { "0b", "8b" },
        { "0c", "8c" },
        { "0d", "8d" },
        { "0e", "8e" },
        { "0f", "8f" },
    };

    private static readonly Dictionary<string, string> Shift4 = new()
    {
        { "00", "87" },
        { "01", "86" },
        { "02", "85" },
        { "03", "84" },
        { "04", "83" },
        { "05", "82" },
        { "06", "81" },
        { "07", "80" },
        { "08", "8f" },
        { "09", "8e" },
        { "0a", "8d" },
        { "0b", "8c" },
        { "0c", "8b" },
        { "0d", "8a" },
        { "0e", "89" },
        { "0f", "88" },
    };
    

    protected abstract (ushort start, ushort end) Range { get; }

    private static byte Shift(ushort page)
    {
        var address = page.ToString("x4");

        var shift = address[1] switch{
            '0' =>short.Parse(Shift0["0" + address[3]], NumberStyles.HexNumber),
            '1' =>short.Parse(Shift1["0" + address[3]], NumberStyles.HexNumber),
            '3' =>short.Parse(Shift3["0" + address[3]], NumberStyles.HexNumber),
            '4' =>short.Parse(Shift4["0" + address[3]], NumberStyles.HexNumber),
            _ => throw new ArgumentOutOfRangeException()
        };
        var result = short.Parse(address[2] + "0", NumberStyles.HexNumber) + shift;
            
        if (result > 255)
            result -= 256;
        
        return (byte)result;
    }

    private byte[] ReadCommand(ushort page)
    {
        var result = new List<byte>();
        
        result.AddRange(new byte[] { 0x23, 0x83, 0x00 });
        result.AddRange(page.ToString("x4").ToBytesFromHexString());
        result.AddRange(new byte[] { 0x00, 0x00, 0x00 });
        result.Add(Shift(page));
        
        return result.ToArray();
    }
    
    protected byte[] WriteCommand(ushort page, byte[] data)
    {
        var result = new List<byte>();
        
        result.AddRange(new byte[] { 0x23, 0x82, 0x00 });
        result.AddRange(page.ToString("x4").ToBytesFromHexString());
        result.AddRange(new byte[] { 0x00, 0x01, 0x00 });
        result.AddRange(data);
        
        byte checksum = Shift(page);
        
        foreach (byte b in data)
        {
            checksum ^= b;
        }

        // byte checksum = CalculateChecksum(result.ToArray());
        
        result.Add(checksum);
        
        return result.ToArray();
    }


    public IEnumerable<byte[]> ReadCommands()
    {
        for (var i = Range.start; i <= Range.end; i++)
        {
            yield return ReadCommand(i);
        }   
    }
    
    public static byte CalculateChecksum(byte[] bytes)
    {
        var commandShift = BitConverter.ToInt16(bytes[3..5].Reverse().ToArray(), 0);
        
        byte checksum = Shift((ushort)commandShift);

        var data = bytes[5..];
        
        foreach (byte b in data)
        {
            checksum ^= b;
        }

        return checksum;
    }

    public abstract IEnumerable<byte[]> WriteCommands();
    public abstract void Parse(IEnumerable<byte[]> blocks);

}