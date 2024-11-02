namespace RtuCommon.Models;

public class ContactInfo
{
    public string Phone { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public bool Always { get; set; }
    
    /// <summary>
    /// Convert contacts to bytes for writing to the device
    /// </summary>
    /// <returns></returns>
    public byte[] ToBytes()
    {
        var bytes = new byte[32];

        if (string.IsNullOrEmpty(Phone))
        {
            return Empty();
        }

        var numberBytes = System.Text.Encoding.Default.GetBytes(Phone);
        Array.Copy(numberBytes, bytes, numberBytes.Length);

        if (Always)
        {
            bytes[22] = 0x63;
            bytes[23] = 0x63;
            bytes[24] = 0x63;
            bytes[25] = 0x63;
            bytes[26] = 0x63;
            bytes[27] = 0x63;
            bytes[28] = 0x63;
            bytes[29] = 0x63;
            bytes[30] = 0x00;
            bytes[31] = 0x63;
        }
        else
        {
            bytes[22] = (byte)StartDateTime!.Value.Month;
            bytes[23] = (byte)StartDateTime!.Value.Day;
            bytes[24] = (byte)StartDateTime!.Value.Hour;
            bytes[25] = (byte)StartDateTime!.Value.Minute;
            bytes[26] = (byte)EndDateTime!.Value.Month;
            bytes[27] = (byte)EndDateTime!.Value.Day;
            bytes[28] = (byte)EndDateTime!.Value.Hour;
            bytes[29] = (byte)EndDateTime!.Value.Minute;
            bytes[30] = (byte)(StartDateTime!.Value.Year - 2000);
            bytes[31] = (byte)(EndDateTime!.Value.Year - 2000);
        }

        return bytes;
    }
    
    public static byte[] Empty()
    {
        var bytes = new byte[32];

        for (var i = 0; i < 32; i++)
        {
            bytes[i] = 0x00;
        }

        return bytes;
    }


}