namespace RtuCommon.Interfaces;

public interface IBlock
{
    /// <summary>
    /// Commands to read data from serial port
    /// </summary>
    /// <returns></returns>
    IEnumerable<byte[]> ReadCommands();
    
    /// <summary>
    /// Commands to write data to serial port
    /// </summary>
    /// <returns></returns>
    IEnumerable<byte[]> WriteCommands();
    
    /// <summary>
    /// Parse data from serial port
    /// </summary>
    /// <param name="blocks"></param>
    void Parse(IEnumerable<byte[]> blocks);
}