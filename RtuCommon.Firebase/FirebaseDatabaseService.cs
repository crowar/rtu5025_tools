using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using RtuCommon.Firebase.Interfaces;

namespace RtuCommon.Firebase;

public class FirebaseDatabaseService : IFirebaseDatabaseService
{
    private readonly FirebaseClient _firebaseClient;

    public FirebaseDatabaseService(string jsonPath, string databaseUrl)
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(jsonPath),
                ProjectId = "rtugate-bef42",
            });
            Console.WriteLine("FirebaseApp успешно инициализирована.");
        }
        
        // Инициализация клиента Firebase
        _firebaseClient = new FirebaseClient(databaseUrl);
    }

    public async Task SaveCollectionAsync<T>(string path, IEnumerable<T> collection)
    {
        try
        {
            var firebaseNode = _firebaseClient.Child(path);

            foreach (var item in collection)
            {
                await firebaseNode.PostAsync(item);
            }

            Console.WriteLine("Коллекция успешно сохранена в Firebase!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении коллекции: {ex.Message}");
        }
    }
}