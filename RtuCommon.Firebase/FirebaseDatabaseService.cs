using System.ComponentModel.DataAnnotations;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RtuCommon.Firebase.Interfaces;

namespace RtuCommon.Firebase;

public class FirebaseDatabaseService(
    string databaseUrl,
    string firebaseSecret,
    ILogger<FirebaseDatabaseService> logger)
    : IFirebaseDatabaseService
{
    private readonly FirebaseClient _firebaseClient = new(databaseUrl,
        new FirebaseOptions
        {
            AuthTokenAsyncFactory = () =>Task.FromResult(firebaseSecret)
        });

    public async Task SaveCollectionAsync<T>(string path, IEnumerable<T> collection) where T : class
    {
        try
        {
            var firebaseNode = _firebaseClient.Child(path);

            foreach (var item in collection)
            {
                var key = GetKeyOrGenerate(item);

                await firebaseNode
                    .Child(key)
                    .PutAsync(JsonConvert.SerializeObject(item));
            }
            
            logger.LogDebug("Коллекция успешно сохранена в Firebase!");
        }
        catch (Exception ex)
        {
            logger.LogError($"Ошибка при сохранении коллекции: {ex.Message}");
        }
    }

    public async Task<IEnumerable<T>> ReadCollectionAsync<T>(string path) where T : class
    {
        try
        {
            var firebaseNode = _firebaseClient.Child(path);

            var data = await firebaseNode.OnceAsync<T>();

            var result = data.Select(item => item.Object);

            logger.LogDebug("Коллекция успешно прочитана из Firebase!");
        
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError($"Ошибка при чтении коллекции: {ex.Message}");
            return [];
        }
    }

    private static string GetKeyOrGenerate(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var type = obj.GetType();

        var keyProperty = type.GetProperties()
            .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

        if (keyProperty == null) 
            return Guid.NewGuid().ToString();
        
        var keyValue = keyProperty.GetValue(obj)?.ToString();
        
        return 
            !string.IsNullOrEmpty(keyValue) 
                ? keyValue 
                : Guid.NewGuid().ToString();
    }
}