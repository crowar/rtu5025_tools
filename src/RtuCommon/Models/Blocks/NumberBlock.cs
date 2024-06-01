using RtuCommon.Extensions;
using RtuCommon.Models.Blocks.Abstracts;

namespace RtuCommon.Models.Blocks;

public sealed class NumberBlock : AbstractBlock
{
    // protected override (ushort start, ushort end) Range => (0x0010, 0x0015);
    protected override (ushort start, ushort end) Range => (0x0010, 0x0186);
    
    public List<ContactInfo> Contacts { get; set; } = new();

    public override IEnumerable<byte[]> WriteCommands()
    {
        if (Contacts.Count > 3000)
        {
            throw new ArgumentOutOfRangeException(nameof(Contacts), "Maximum number of contacts is 3000");
        }

        var blockLimit = 8;
        
        for (var page = Range.start; page <= Range.end; page++)
        {
            var data = new List<byte>();

            foreach (var item in Contacts.Skip((page-Range.start)*blockLimit).Take(blockLimit))
            {
                data.AddRange(item.ToBytes());
            }
            
            while (data.Count < 256)
            {
                data.Add(0x00);
            }

            yield return WriteCommand(page, data.ToArray());
        }   
        
    }

    public override void Parse(IEnumerable<byte[]> blocks)
    {
        foreach (var bytes in blocks)
        {
            var header = bytes.GetSlice(0, 8);
            var data = bytes.GetSlice(header.LastIndex, 256);
            var checksum = bytes.GetSlice(data.LastIndex, 1); //TODO: Have to check if this is correct
            
            var position = 0;
            var numberBlockLength = 32;
        
            while (position <= data.Result.Length-numberBlockLength)
            {
                var numberRecord = data.Result.GetSlice(position, numberBlockLength);
                position = numberRecord.LastIndex;

                var number = numberRecord.Result[..22];

                if (number[0] == 0x00 || number[0] == 0xff)
                {
                    continue;
                }

                var startDateMonth = numberRecord.Result[22];
                var startDateDay = numberRecord.Result[23];
                var startDateHour = numberRecord.Result[24];
                var startDateMinute = numberRecord.Result[25];
                var endDateMonth = numberRecord.Result[26];
                var endDateDay = numberRecord.Result[27];
                var endDateHour = numberRecord.Result[28];
                var endDateMinute = numberRecord.Result[29];
                var startDateYear = numberRecord.Result[30];
                var endDateYear = numberRecord.Result[31];

                var contact = new ContactInfo
                {
                    Phone = System.Text.Encoding.Default.GetString(number.CleanAfterZero())
                };
                
                if (startDateYear == 0x00 && endDateYear == 0x63)
                {
                    contact.Always = true;
                    contact.StartDateTime = null;
                    contact.EndDateTime = null;
                }
                else
                {
                    contact.Always = false;
                    contact.StartDateTime = new DateTime(startDateYear + 2000, startDateMonth, startDateDay, startDateHour,
                        startDateMinute, 0, DateTimeKind.Local);
                    contact.EndDateTime = new DateTime(endDateYear + 2000, endDateMonth, endDateDay, endDateHour, endDateMinute, 0,
                        DateTimeKind.Local);
                }
                
                Contacts.Add(contact);

            }
            
        }
    }
}