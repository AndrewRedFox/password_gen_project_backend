using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using password_gen_project_backend.Enum;

//namespace password_gen_project_backend.UserModel
public class UserModel
{
    public UserModel (int id, string login, string password)
    {
        this.id = id;
        this.login = login;
        this.password = password;
        role = roles.USER;
        listOfPassword = new List<Pair<string, string>> ();
        listOfPassword.Add (new Pair<string, string> ("loginApp", "passwordApp"));
    }

    public UserModel()
    {

    }

    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public int id { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public roles role { get; set; }
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
    public List<Pair<string , string>> listOfPassword {  get; set; }
}

