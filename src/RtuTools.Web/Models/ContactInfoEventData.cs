using RtuCommon.Models;

namespace RtuTools.Web.Models;

public class ContactInfoEventData
{
    public EventType EventType { get; set; }

    public string OldPhone { get; set; }

    public ContactInfo ContactInfo { get; set; }
}