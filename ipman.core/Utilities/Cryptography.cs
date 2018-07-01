using System;
using System.Security.Cryptography;
using System.Text;

namespace ipman.core.Utilities
{
    public static class Cryptography
    {
        #region Constants

        private const int DefaultHashIterations = 1000;
        private const int SaltSize = 16;

        #endregion

        #region Fields

        /// <summary>
        /// Source for crypto-grade random numbers.
        ///
        /// Should only be accessed through the Random() method for locking purposes.
        /// </summary>
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        #endregion

        #region Methods

        /// <summary>
        /// Crypto-grade, thread-safe random number generator.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] Random(int bytes)
        {
            var output = new byte[bytes];

            lock (RandomNumberGenerator) RandomNumberGenerator.GetBytes(output);

            return output;
        }

        /// <summary>
        /// Generate a random salt.
        ///
        /// The format for salts is [number of iterations in hex].[salt number] (without the brackets).
        /// </summary>
        /// <param name="explicitIterations"></param>
        /// <returns></returns>
        public static string GenerateSalt(int? explicitIterations = null)
        {
            if (explicitIterations.HasValue && explicitIterations.Value < DefaultHashIterations)
                throw new ArgumentOutOfRangeException("explicitIterations", explicitIterations.Value,
                                                      "Cannot be less than " + DefaultHashIterations);

            var bytes = Random(SaltSize);

            var iterations = (explicitIterations ?? DefaultHashIterations).ToString("X");

            return iterations + "." + Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Securely hash a string using the specified salt.
        ///
        /// The salt is expected to be in the same format as returned by the GenerateSalt() method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Hash(string value, string salt)
        {
            var i = salt.IndexOf('.');
            var iterations = int.Parse(salt.Substring(0, i), System.Globalization.NumberStyles.HexNumber);

            salt = salt.Substring(i + 1);

            using (var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(value), Convert.FromBase64String(salt), iterations))
            {
                var key = pbkdf2.GetBytes(24);

                return Convert.ToBase64String(key);
            }
        }

        /// <summary>
        /// JRS 05-02-2012 - generates a simple hash from a passed string. NEVER USE THIS FOR PASSWORDS!
        /// </summary>
        /// <param name="stringToHash"></param>
        /// <returns></returns>
        public static string SimpleHash(string stringToHash)
        {
            // return the hash value as a string
            return BitConverter.ToString(SimpleHashInternal(stringToHash));
        }

        /// <summary>
        /// JRS 05-02-2012 - generates a simple hash from passed bytes. NEVER USE THIS FOR PASSWORDS!
        /// </summary>
        /// <param name="bytesToHash"></param>
        /// <returns></returns>
        public static string SimpleHash(byte[] bytesToHash)
        {
            // return the hash value as a string
            return BitConverter.ToString(SimpleHashInternal(bytesToHash));
        }

        /// <summary>
        /// JRS 05-02-2012 - generates a simple base64 encoded hash from a passed string. NEVER USE THIS FOR PASSWORDS!
        /// </summary>
        /// <param name="stringToHash"></param>
        /// <returns></returns>
        public static string SimpleHashBase64(string stringToHash)
        {
            // return the hash value as a string 
            return Convert.ToBase64String(SimpleHashInternal(stringToHash));
        }

        /// <summary>
        /// JRS 05-02-2012 - generates a simple base64 encoded hash from passed bytes. NEVER USE THIS FOR PASSWORDS!
        /// </summary>
        /// <param name="bytesToHash"></param>
        /// <returns></returns>
        public static string SimpleHashBase64(byte[] bytesToHash)
        {
            // return the hash value as a string 
            return Convert.ToBase64String(SimpleHashInternal(bytesToHash));
        }

        private static byte[] SimpleHashInternal(string stringToHash)
        {
            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = Encoding.UTF8.GetBytes(stringToHash);
            // Compute the Hash. This returns an array of Bytes.
            return hashAlg.ComputeHash(bytValue);
        }

        private static byte[] SimpleHashInternal(byte[] bytesToHash)
        {
            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Compute the Hash. This returns an array of Bytes.
            return hashAlg.ComputeHash(bytesToHash);
        }

        #endregion
    }
}
