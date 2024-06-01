using RtuCommon.Extensions;
using RtuCommon.Models.Blocks.Abstracts;

namespace RtuCommon.Models.Blocks;

public sealed class HistoryBlock : AbstractBlock
{
    protected override (ushort start, ushort end) Range => (0x0300, 0x04F3);

    public List<HistoryRecord> History { get; set; } = new();
    
    public override IEnumerable<byte[]> WriteCommands()
    {
        throw new NotImplementedException();
    }

    public override void Parse(IEnumerable<byte[]> blocks)
    {
        foreach (var block in blocks)
        {
            var header = block.GetSlice(0, 8);
            var data = block.GetSlice(header.LastIndex, 256);
            var checksum = block.GetSlice(data.LastIndex, 1); //TODO: Have to check if this is correct
            
            var position = 0;
            var historyBlockLength = 128;

            while (position <= data.Result.Length - historyBlockLength)
            {
                var historySlice = data.Result.GetSlice(position, historyBlockLength);
                position = historySlice.LastIndex;

                var historyRecord = historySlice.Result;
                
                if (historyRecord[0] == 0x00 || historyRecord[0] == 0xff)
                {
                    continue;
                }

                History.Add(new HistoryRecord
                {
                    EventNumber = historyRecord[0],
                    EventTime = new DateTime(historyRecord[4] + 2000, historyRecord[5], historyRecord[6], 
                        historyRecord[7], historyRecord[8], historyRecord[9], DateTimeKind.Local)
                });

            }
        }
    }
}