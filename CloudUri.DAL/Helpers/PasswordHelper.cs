using System;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace CloudUri.DAL.Helpers
{
    /// <summary>
    /// Incapsulates the logic of password creating and checking
    /// </summary>
    public class PasswordHelper
    {
        static PasswordHelper()
        {
            PasswordEncoding = Encoding.Default;
        }

        /// <summary>
        /// Gets the password encoding
        /// </summary>
        public static Encoding PasswordEncoding { get; private set; }

        /// <summary>
        /// Salt length
        /// </summary>
        public const int SaltLength = 32;

        /// <summary>
        /// Converts password to hash and salt
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="hash">Created hash</param>
        /// <param name="salt">Generated salt</param>
        public static void CreateHashAndSalt(string password, out string hash, out string salt)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var saltBytes = CreateSalt(PasswordEncoding.IsSingleByte ? SaltLength : SaltLength*2);
            var passBytes = PasswordEncoding.GetBytes(password);

            salt = PasswordEncoding.GetString(saltBytes);
            hash = PasswordEncoding.GetString(GenerateSaltedHash(passBytes, saltBytes));
        }

        /// <summary>
        /// Compares entered string password with hash value
        /// </summary>
        /// <param name="hash">Salted hash of password</param>
        /// <param name="password">String password to compare</param>
        /// <param name="salt">Salt</param>
        /// <returns>Returns true if salted hash is the same as the entered password</returns>
        public static bool ComparePasswordAndHash(string hash, string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            var passBytes = PasswordEncoding.GetBytes(password);
            var hashedPassword = GenerateSaltedHash(passBytes, PasswordEncoding.GetBytes(salt));
            return PasswordEncoding.GetBytes(hash).SequenceEqual(hashedPassword);
        }

        /// <summary>
        /// Generates new salt
        /// </summary>
        /// <param name="size">Salt size</param>
        /// <returns>New salt</returns>
        internal static byte[] CreateSalt(int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException("size", size, "Size must be positive");

            using (var rng = new RNGCryptoServiceProvider())
            {
                var buff = new byte[size];
                rng.GetBytes(buff);

                return buff;
            }
        }

        /// <summary>
        /// Appends password and salt
        /// </summary>
        /// <param name="plainText">Password presented in bytes array</param>
        /// <param name="salt">Salt for password</param>
        /// <returns>Salted password</returns>
        internal static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            var plainTextWithSaltBytes = plainText.Concat(salt).ToArray();

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }
}