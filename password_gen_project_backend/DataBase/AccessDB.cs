using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
public class AccessDB
{
    private const string connectionString = "mongodb://localhost:27017";
    private const string databaseName = "simple_db";
    private const string UserCollection = "users";
    private const string IdCollection = "ids";

    private IMongoCollection<T> connectToMongo<T>(in string collection)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        return db.GetCollection<T>(collection);
    }

    public async Task<List<UserModel>> GetAllUsers()
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var result = await userCollection.FindAsync(_ => true);
        return result.ToList();
    }

    public async Task<List<UserModel>> getUserByUser(UserModel user)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var result = await userCollection.FindAsync(c =>  c.login == user.login);
        return result.ToList();
    }

    public async Task<List<UserModel>> getUserByLogin(string login)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var result = await userCollection.FindAsync(c => c.login == login);
        return result.ToList();
    }

    public Task createUser(UserModel user)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        return userCollection.InsertOneAsync(user);
    }

    public Task updateList(UserModel user)
    {
        var userCollection = connectToMongo<UserModel>(UserCollection);
        var filter = Builders<UserModel>.Filter.Eq("login", user.login);
        return userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = false });
    }
}

