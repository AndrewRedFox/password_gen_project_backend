using password_gen_project_backend.Enum;

//namespace password_gen_project_backend.UserModel
public class UserModel
{
    public UserModel (string login, string password)
    {
        this.login = login;
        this.password = password;
        role = roles.USER;
        listOfPassword = new Dictionary<string, string> { ["login"]= "password" };
    } 

        
    public long id { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public roles role { get; set; }
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
    public Dictionary<string , string> listOfPassword {  get; set; }
}

