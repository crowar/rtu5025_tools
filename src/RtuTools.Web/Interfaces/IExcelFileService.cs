namespace RtuTools.Web.Interfaces;

/// <summary>
/// Represents a service for exporting data to a file.
/// </summary>
public interface IExcelFileService
{
    /// <summary>
    /// Saves the given collection of report items to a file in Excel format.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="items">The collection of report items to be saved.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    Task<byte[]> GetFileData<T>(IEnumerable<T> items, CancellationToken token = default);
    
    Task<IEnumerable<T>> GetCollections<T>(byte[] fileData, CancellationToken token = default) where T : new();
}