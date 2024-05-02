using System.Security.Cryptography;
using System.Text;

namespace UsersAPI.Utils
{
    public static class Utils
    {
        public static string HashPassword(string password)
        {
            using (var algorithm = SHA256.Create())
            {
                // Convert the password string to byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = algorithm.ComputeHash(passwordBytes);

                // Convert the hash to string and return
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
