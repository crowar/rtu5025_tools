namespace RtuCommon.Firebase.Interfaces;

public interface IFirebaseDatabaseService
{
    Task SaveCollectionAsync<T>(string path, IEnumerable<T> collection) where T : class;

    Task<IEnumerable<T>> ReadCollectionAsync<T>(string path) where T : class;
}