namespace RtuCommon.Models;

public class HistoryRecord
{
    public int EventNumber { get; set; }
    public DateTime EventTime { get; set; }
    public string From { get; set; }
    public string Action { get; set; }
    public string? Executant { get; set; }
    public string? Contact { get; set; }
}