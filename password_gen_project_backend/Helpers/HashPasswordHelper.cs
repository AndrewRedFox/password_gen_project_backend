using System.Security.Cryptography;
using System.Text;

namespace password_gen_project_backend.Helpers
{
    public abstract class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
            
        }
    }
}
