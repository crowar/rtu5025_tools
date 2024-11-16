namespace RtuCommon.Firebase.Interfaces;

public interface IFirebaseDatabaseService
{
    // Task<IEnumerable<T>> ReadAsync<T>() where T : class;
    
    // void WriteAsync<T>(IEnumerable<T> data, CancellationToken token = default) where T : class;
    
    Task SaveCollectionAsync<T>(string path, IEnumerable<T> collection);

}
