using OfficeOpenXml;
using OfficeOpenXml.Table;
using RtuTools.Web.Extantions;
using RtuTools.Web.Interfaces;
using RtuTools.Web.Models;

namespace RtuTools.Web.Services;

/// <summary>
/// Represents a service for exporting data to a file.
/// </summary>
public class ExcelFileService : IExcelFileService
{
    private ILogger<ExcelFileService> _logger;

    /// <summary>
    /// CTOR
    /// </summary>
    public ExcelFileService(ILogger<ExcelFileService> logger)
    {
        _logger = logger;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    /// <summary>
    /// Retrieves the file data for the given collection of items in Excel format.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="items">The collection of items to be saved.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The byte array representing the Excel file data.</returns>
    public async Task<byte[]> GetFileData<T>(IEnumerable<T> items, CancellationToken token = default)
    {
        using var pck = new ExcelPackage();

        var ws = pck.Workbook.Worksheets.Add(typeof(T).Name);
        ws.Cells["A1"].LoadFromCollection(items, PrintHeaders: true, TableStyles.Medium21);
        ws.Cells[ws.Dimension.Address].AutoFitColumns();

        using var stream = new MemoryStream();
        await pck.SaveAsAsync(stream, token);

        return stream.ToArray();
    }

    public async Task<IEnumerable<T>> GetCollections<T>(byte[] fileData, CancellationToken token = default) where T : new()
    {
        using var stream = new MemoryStream(fileData);
        using var pck = new ExcelPackage(stream);

        var ws = pck.Workbook.Worksheets.First();
        var collections = ws.Tables.First().ConvertTableToObjects<T>().ToList();

        return collections;
    }
}