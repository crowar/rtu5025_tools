using System.Globalization;
using RtuCommon.Models;

namespace RtuTools.Web.Models;

public class ContactInfoReport
{
    public ContactInfoReport()
    {
    }

    public ContactInfoReport(ContactInfo ci)
    {
        Phone = ci.Phone;
        StartDateTime = ci.StartDateTime?.ToString("yyyy-MM-dd HH:mm:ss");
        EndDateTime = ci.EndDateTime?.ToString("yyyy-MM-dd HH:mm:ss");
        Always = ci.Always ? "Yes" : "No";
    }

    public string Phone { get; set; }
    public string? StartDateTime { get; set; }
    public string? EndDateTime { get; set; }
    public string Always { get; set; }

    public ContactInfo ToContactInfo()
    {
        var ci = new ContactInfo
        {
            Phone = Phone
        };

        if (Always.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
        {
            ci.Always = true;
        }
        else
        {
            DateTime.TryParseExact(StartDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDateTime);
            DateTime.TryParseExact(EndDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDateTime);

            ci.StartDateTime = startDateTime;
            ci.EndDateTime = endDateTime;
        }

        return ci;
    }
}