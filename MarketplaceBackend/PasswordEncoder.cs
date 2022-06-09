using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MarketplaceBackend
{
    public static class PasswordEncoder
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }
        public static string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        public static bool ValidatePassword(string password, string hash, string salt)
        {
            string newHash = HashPassword(password, Convert.FromBase64String(salt));
            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != newHash[i])
                    return false;
            }
            return true;
        }
    }
}
