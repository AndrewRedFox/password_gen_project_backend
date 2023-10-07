using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class CreateTokens
{
    UserModel user = new UserModel();
    public string createAccessToken(UserModel user)
    {
        string accessToken = "";
        this.user = user;

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(SecretKey.secretKey);


        var descriptor = new SecurityTokenDescriptor
        {
            Audience = "client",
            Issuer = user.login,
            Expires = DateTime.Now.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };

        SecurityToken token = tokenHandler.CreateToken(descriptor);
        accessToken = tokenHandler.WriteToken(token);

        return accessToken;
    }

    public string createRefreshToken(UserModel user)
    {
        string refreshToken = "";
        this.user = user;

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(SecretKey.secretKey);

        var descriptor = new SecurityTokenDescriptor
        {
            Audience = "client",
            Issuer = user.login,
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };

        SecurityToken token = tokenHandler.CreateToken(descriptor);
        refreshToken = tokenHandler.WriteToken(token);

        return refreshToken;
    }
}