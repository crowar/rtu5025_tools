using System.IO.Ports;
using Microsoft.Extensions.Logging;
using RtuCommon.Extensions;
using RtuCommon.Interfaces;
using RtuCommon.Models.Blocks.Abstracts;

namespace RtuCommon;

public class RtuModule(ILogger logger, string portName, int portSpeed = RtuModule.PortSpeed) : IRtuModule
{
    private readonly SerialPort _serialPort = new(portName, portSpeed, Parity.None);

    private const int PortSpeed = 115400;

    public void Connect()
    {
        if (!_serialPort.IsOpen)
        {
            _serialPort.Open();
        }
    }

    public void Disconnect()
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Close();
        }
    }

    public T Read<T>() where T : IBlock
    {
        var obj = Activator.CreateInstance<T>();
        var commands = obj.ReadCommands();
        var resultBytes = commands.Select(SendCommand).ToList();
        obj.Parse(resultBytes);
        return obj;
    }

    public void Write<T>(T obj) where T : IBlock
    {
        var commands = obj.WriteCommands().ToList();
        foreach (var command in commands)
        {
            var answer = SendCommand(command);
        }
    }

    private byte[] SendCommand(byte[] command)
    {
        _serialPort.Write(command, 0, command.Length);

        Thread.Sleep(100);

        var bytesToRead = _serialPort.BytesToRead;
        var buffer = new byte[bytesToRead];
        _serialPort.Read(buffer, 0, bytesToRead);

        var isRqValid = command.Any() && AbstractBlock.CalculateChecksum(command[..^1]) == command[^1];
        var isRsValid = buffer.Any() && AbstractBlock.CalculateChecksum(buffer[..^1]) == buffer[^1];

        logger.LogDebug(">>>{CheckResult} {HexString}", isRqValid ? "+" : "-", command.ToHexString());
        logger.LogDebug("<<<{CheckResult} {HexString}", isRsValid ? "+" : "-", buffer.ToHexString());

        return buffer;
    }
}

public static class RtuHelper
{
    public static List<String> GetAvailablePorts()
    {
        return SerialPort.GetPortNames().ToList();
    }
}