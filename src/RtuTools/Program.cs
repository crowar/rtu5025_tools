using System.IO.Ports;
using Microsoft.Extensions.Logging;
using RtuCommon;
using RtuCommon.Models.Blocks;

public static class Program
{
#if WINDOWS
    private const string PortName = "COM3";
#else
    private const string PortName = "/dev/tty.usbserial-0001";
#endif

    private const int PortSpeed = 115400;
    
    public static void Main(string[] args)
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger("Program");
        
        // val rtuDevice = devices.firstOrNull { it.vendorID == 4292 && it.productID == 60000 }
        
        var module = new RtuModule(logger, PortName, PortSpeed);
        
        module.Connect();
        
        var settings = module.Read<SettingBlock>();
        var number = module.Read<NumberBlock>();
        // var history = module.Read<HistoryBlock>();


        // var number = new NumberBlock();
        // number.Contacts.Add(new ContactInfo(){Phone = "87001884433", Always = true});
        // number.Contacts.Add(new ContactInfo(){Phone = "87007335500", Always = true});
        // number.Contacts.Add(new ContactInfo(){Phone = "87010487757", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now.AddYears(1), Always = false});
        //
        // module.Write(number);
        

        module.Disconnect();
    }
}