using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WcfCommCrypto
{
    public class RijndaelManagedEncryption
    {
        #region Rijndael Encryption

        /// <summary>
        ///     Encrypt the given text and give the byte array back as a BASE64 string
        /// </summary>
        /// <param name="text" />
        /// <param name="saltKey"></param>
        /// <param name="inputKey"></param>
        /// The text to encrypt
        /// <returns>The encrypted text</returns>
        public static string EncryptRijndael(string text, string saltKey, string inputKey)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            var aesAlg = NewRijndaelManaged(saltKey, inputKey);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        #endregion

        #region NewRijndaelManaged

        /// <summary>
        ///     Create a new RijndaelManaged class and initialize it
        /// </summary>
        /// <param name="salt" />
        /// <param name="inputKey"></param>
        /// The pasword salt
        /// <returns/>
        private static RijndaelManaged NewRijndaelManaged(string salt, string inputKey)
        {
            if (salt == null) throw new ArgumentNullException(nameof(salt));
            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(inputKey, saltBytes);
            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize/8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize/8);

            return aesAlg;
        }

        #endregion

        #region Rijndael Dycryption

        /// <summary>
        ///     Checks if a string is base64 encoded
        /// </summary>
        /// <param name="base64String" />
        /// The base64 encoded string
        /// <returns/>
        public static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length%4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        /// <summary>
        ///     Decrypts the given text
        /// </summary>
        /// <param name="cipherText" />
        /// <param name="saltKey"></param>
        /// <param name="inputKey"></param>
        /// The encrypted BASE64 text
        /// <returns>De gedecrypte text</returns>
        public static string DecryptRijndael(string cipherText, string saltKey, string inputKey)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            if (!IsBase64String(cipherText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var aesAlg = NewRijndaelManaged(saltKey, inputKey);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(cipherText);
            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }
            return text;
        }

        #endregion
    }
}