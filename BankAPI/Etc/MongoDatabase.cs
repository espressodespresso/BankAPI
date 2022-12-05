using MongoDB.Bson;
using MongoDB.Driver;

namespace BankAPI;

public class MongoDatabase
{ 
    private static string connectionString = "CONNECTIONSTRINGHERE";
    private static MongoClient mongoClient;
    private IMongoDatabase db;

    public MongoDatabase()
    { 
        try 
        { 
            mongoClient = new MongoClient(connectionString); 
            db = mongoClient.GetDatabase("Bank");
            Console.WriteLine("Connected");
        }
        catch (Exception)
        { 
            Environment.Exit(0);
        }
    }

    public void InsertRecord<T>(string table, T record)
    { 
        var collection = db.GetCollection<T>(table); 
        collection.InsertOne(record);
    }

    public T LoadRecord<T>(string collectionName,string field, string info)
    { 
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info);
        return collection.Find(filter).First();
    }
        
    public T LoadRecordByID<T>(string collectionName,string field, int info)
    {
        var collection = db.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(field, info); 
        return collection.Find(filter).First();
    }
    
    public T LoadCard<T>(string collectionName,string field, long info)
    {
        var collection = db.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(field, info); 
        return collection.Find(filter).First();
    }

    public List<T> LoadAccounts<T>(string field, int info)
    {
        var collection = db.GetCollection<T>("Accounts");
        var filter = Builders<T>.Filter.Eq(field, info);
        return collection.Find(filter).ToList();
    }

    public List<T> LoadStatement<T>(string field, int info)
    {
        var collection = db.GetCollection<T>("Transactions");
        var filter = Builders<T>.Filter.Eq(field, info);
        return collection.Find(filter).Limit(10).ToList();
    }
        
    public void DeleteRecord<T>(string collectionName, string field, string info)
    { 
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        collection.DeleteOne(filter);
    }

    public void UpdateRecord<T>(string collectionName, string field, int info, string change, float changeinfo)
    { 
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        var update = Builders<T>.Update.Set(change, changeinfo); 
        collection.UpdateOne(filter, update);
    }

    public void UpdateStringRecord<T>(string collectionName, string field, int info, string change, string changeinfo)
    {
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        var update = Builders<T>.Update.Set(change, changeinfo); 
        collection.UpdateOne(filter, update);
    }
    
    public void UpdateDateTimeRecord<T>(string collectionName, string field, int info, string change, DateTime changeinfo)
    {
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        var update = Builders<T>.Update.Set(change, changeinfo); 
        collection.UpdateOne(filter, update);
    }
    
    public void UpdateCardRecord<T>(string collectionName, string field, long info, string change, float changeinfo)
    { 
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        var update = Builders<T>.Update.Set(change, changeinfo); 
        collection.UpdateOne(filter, update);
    }
    
    public void UpdateBoolRecord<T>(string collectionName, string field, int info, string change, bool changeinfo)
    { 
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        var update = Builders<T>.Update.Set(change, changeinfo); 
        collection.UpdateOne(filter, update);
    }
        
    public void UpdateArrayRecord<T>(string collectionName, string field, string info, string change, BsonArray changeinfo)
    {
        var collection = db.GetCollection<T>(collectionName); 
        var filter = Builders<T>.Filter.Eq(field, info); 
        var update = Builders<T>.Update.Set(change, changeinfo); 
        collection.UpdateOne(filter, update);
    }

    public List<T> LoadAll<T>(string collectionName) 
    {
       var documents = db.GetCollection<T>(collectionName).Find(new BsonDocument()).ToList(); 
       return documents;
    }
}