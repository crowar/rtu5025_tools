using System.Globalization;
using RtuTools.Extensions;

namespace CheckSum.Extensions;

public static class CommandExtension
{
    private static Dictionary<string, string> _shift0 = new() {
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

    private static Dictionary<string, string> _shift1 = new() {
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

    private static Dictionary<string, string> _shift3 = new() {
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

    private static Dictionary<string, string> _shift4 = new() {
        {"00", "87"},
        {"01", "86"},
        {"02", "85"},
        {"03", "84"},
        {"04", "83"},
        {"05", "82"},
        {"06", "81"},
        {"07", "80"},
        {"08", "8f"},
        {"09", "8e"},
        {"0a", "8d"},
        {"0b", "8c"},
        {"0c", "8b"},
        {"0d", "8a"},
        {"0e", "89"},
        {"0f", "88"},
    };
    
    public static byte GetCommandShift(this int page)
    {
        var address = page.ToString("x4");

        var shift = address[1] switch{
            '0' =>short.Parse(_shift0["0" + address[3]], NumberStyles.HexNumber),
            '1' =>short.Parse(_shift1["0" + address[3]], NumberStyles.HexNumber),
            '3' =>short.Parse(_shift3["0" + address[3]], NumberStyles.HexNumber),
            '4' =>short.Parse(_shift4["0" + address[3]], NumberStyles.HexNumber),
            _ => throw new ArgumentOutOfRangeException()
        };
        var commandShift = short.Parse(address[2] + "0", NumberStyles.HexNumber) + shift;
            
        if (commandShift > 255)
            commandShift -= 256;
        
        return (byte)commandShift;
    }
    
    public static int GetIntFromBytes(this byte[] bytes)
    {
        return BitConverter.ToInt16(bytes.Reverse().ToArray(), 0);
    }
}