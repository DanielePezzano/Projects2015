using System.Security.Cryptography;
using System.Text;

namespace WcfCommCrypto
{
    public static class Sha1Managed
    {
        /// <summary>
        ///     take any string and encrypt it using SHA1 then
        ///     return the encrypted data
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public static string GetSha1HashData(string data)
        {
            //create new instance of SHA1
            var sha1 = SHA1.Create();

            //convert the input text to array of bytes
            var hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            var returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            foreach (var t in hashData)
            {
                returnValue.Append(t.ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }

        /// <summary>
        ///     encrypt input text using SHA1 and compare it with
        ///     the stored encrypted text
        /// </summary>
        /// <param name="inputData">input text you will enterd to encrypt it</param>
        /// <param name="storedHashData">
        ///     the encrypted
        ///     text stored on file or database ... etc
        /// </param>
        /// <returns>true or false depending on input validation</returns>
        public static bool ValidateSha1HashData(string inputData, string storedHashData)
        {
            //hash input text and save it string variable
            var getHashInputData = GetSha1HashData(inputData);

            return string.CompareOrdinal(getHashInputData, storedHashData) == 0;
        }
    }
}