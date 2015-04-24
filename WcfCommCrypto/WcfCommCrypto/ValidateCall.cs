using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfCommCrypto
{
    public static class ValidateCall
    {
        /// <summary>
        /// Valida una chiamata entrante
        /// </summary>
        /// <param name="clearDatetime">data in chiaro</param>
        /// <param name="authHash">stringa criptata per validazione lato server</param>
        /// <param name="clientHash">stringa criptata per riconoscimento client</param>
        /// <param name="saltKey">chiave salt per decriptazione simmetrica</param>
        /// <param name="inputKey">chiave di decriptazione simmetrica</param>
        /// <param name="dtoSignature">identificativo del DTO richiesto dal client</param>
        /// <param name="username">username per autenticazione</param>
        /// <param name="password">password per autenticazione</param>
        /// <param name="acceptedClients">client riconosciuti dal server</param>
        /// <returns></returns>
        public static bool Validate(
            DateTime clearDatetime,
            string authHash,
            string clientHash,
            string saltKey,
            string inputKey,
            string dtoSignature,
            string username,
            string password,
            ReadOnlyCollection<string> acceptedClients,
            char separator)
        {
            bool result = false;
            string CompareAuthHash = username + separator + password + separator + dtoSignature + separator + clearDatetime.ToUniversalTime();
            string CompareClientHash = RijndaelManagedEncryption.DecryptRijndael(clientHash, saltKey, inputKey);//Client_datetime_firmaDto

            if (!string.IsNullOrEmpty(CompareAuthHash) && !string.IsNullOrEmpty(CompareClientHash))
            {
                string[] clientHashSequence = CompareClientHash.Split(separator);
                /*
                 * [0]=>client
                 * [1]=>datetime
                 * [2]=>firma DTO
                 */

                if (clientHashSequence.Length == 3 && acceptedClients.Contains(clientHashSequence[0]))
                {
                    if (!string.IsNullOrEmpty(clientHashSequence[1]))
                    {
                        try
                        {
                            DateTime serverTime = Convert.ToDateTime(clientHashSequence[1]);
                            if ((serverTime - DateTime.Now).Minutes <= 1 &&
                                (serverTime - DateTime.Now).Minutes >= 0 &&
                                Sha1Managed.ValidateSHA1HashData(CompareAuthHash, authHash))
                                result = true;
                        }
                        catch (Exception ex)
                        {
                            //DA FARE
                            //IMPLEMENTA UN SISTEMA DI LOG (UNA LIBRERIA DI CLASSI CONDIVISA)
                        }
                    }
                }
            }
            return result;
        }
    }
}
