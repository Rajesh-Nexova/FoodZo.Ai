using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace FoodZOAI.UserManagement.Utils
{
    public static class PasswordHelper
    {
        // Generate a random salt
        public static string GenerateSalt(int size = 16)
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        // Hash password using Argon2id
        public static string HashPassword(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);

            using var argon2 = new Argon2id(passwordBytes)
            {
                Salt = saltBytes,
                DegreeOfParallelism = 4, // Number of threads
                MemorySize = 65536,      // 64 MB
                Iterations = 4           // Number of iterations
            };

            var hashBytes = argon2.GetBytes(32); // 256-bit hash
            return Convert.ToBase64String(hashBytes);
        }

        // Verify password
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var enteredHash = HashPassword(enteredPassword, storedSalt);

            Console.WriteLine("EnteredHash: " + enteredHash); // Debug
            Console.WriteLine("StoredHash:  " + storedHash);  // Debug

            return enteredHash == storedHash;
        }
    }
}
