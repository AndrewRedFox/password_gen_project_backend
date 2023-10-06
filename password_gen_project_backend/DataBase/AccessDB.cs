using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
public class AccessDB
{
    private const string connectionString = "mongodb://localhost:27017";
    private const string databaseName = "simple_db";
    private const string userCollection = "users";

    private IMongoCollection<T> connectToMongo<T>(in string collection)
    {
        var client = MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        return db.GetCollection<T>(collection);
    }

    public async Task<List<UserModel>> GetAllUsers()
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var result = await userCollection.FindAsync(_ => true);
        return result.ToList();
    }

    public async Task<List<UserModel>> getUserByLogin(UserModel user)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var result = await userCollection.FindAsync(c =>  c.login == user.login);
        return result.ToList();
    }

    public Task createUser(UserModel user)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        return userCollection.InsertOneAsync(user);
    }

    public Task updateListOfPassword(UserModel user)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var filter = Builders<UserModel>.Filter.Eq("id", user.id);
        return userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
    }


}

