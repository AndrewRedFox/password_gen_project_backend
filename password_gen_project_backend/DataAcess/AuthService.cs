using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace password_gen_project_backend.DataAcess
{
    public abstract class AuthService
    {
        private static string secretKey = "sdad-dasdas-fghgklkl456";
        public static string GenerateJWT(string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Audience = "client",
                Issuer = "ApiToken",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = tokenHandler.CreateToken(descriptor);

           return tokenHandler.WriteToken (token);
        }
    }
}
