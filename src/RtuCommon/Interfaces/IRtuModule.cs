namespace RtuCommon.Interfaces;

/// <summary>
/// Represents a module for reading and writing data from a serial port.
/// </summary>
public interface IRtuModule
{
    /// <summary>
    /// Connects to the serial port.
    /// </summary>
    void Connect();

    /// <summary>
    /// Disconnects from the serial port. If the serial port is open, it is closed.
    /// </summary>
    void Disconnect();


    /// <summary>
    /// Reads data from the serial port and returns an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of object to be returned. Must implement the IBlock interface.</typeparam>
    /// <returns>An object of type T.</returns>
    T Read<T>() where T : IBlock;

    /// <summary>
    /// Writes data to the serial port. The data is provided as an object of type T which must implement the IBlock interface.
    /// </summary>
    /// <typeparam name="T">The type of object to be written. Must implement the IBlock interface.</typeparam>
    /// <param name="obj">The object of type T to be written.</param>
    /// <remarks>
    /// This method sends the write commands returned by the WriteCommands method of the provided object T to the serial port.
    /// The WriteCommands method should return an IEnumerable<byte[]> representing the write commands.
    /// Each write command is sent to the serial port using the SendCommand method, and the response is checked if needed.
    /// </remarks>
    void Write<T>(T obj) where T : IBlock;
}