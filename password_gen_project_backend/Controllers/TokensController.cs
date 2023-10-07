using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using password_gen_project_backend.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

public class TokensController
{
    public bool validate(string Token, string validIssuer, string dbToken)
    {
        //сравнить токен в БД и у пользователя
        if (!Equals(Token, dbToken))
            return false;
        

        TokenValidationParameters validationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = validIssuer,
            ValidAudience = "client",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey.secretKey)),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(Token, validationParameters, out validatedToken);
            //Console.WriteLine(principal.Identity.IsAuthenticated);//true
            //Console.WriteLine(validatedToken);

        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
}