﻿using System;
using System.Collections.ObjectModel;

namespace WcfCommCrypto
{
    public static class ValidateCall
    {
        /// <summary>
        ///     Valida una chiamata entrante
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
        /// <param name="separator"></param>
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
            var result = false;
            var compareAuthHash = username + separator + password + separator + dtoSignature + separator +
                                  clearDatetime.ToUniversalTime();
            var compareClientHash = RijndaelManagedEncryption.DecryptRijndael(clientHash, saltKey, inputKey);
                //Client_datetime_firmaDto

            if (string.IsNullOrEmpty(compareAuthHash) || string.IsNullOrEmpty(compareClientHash)) return false;
            var clientHashSequence = compareClientHash.Split(separator);
            /*
                 * [0]=>client
                 * [1]=>datetime
                 * [2]=>firma DTO
                 */

            if (clientHashSequence.Length != 3 || !acceptedClients.Contains(clientHashSequence[0])) return false;
            if (string.IsNullOrEmpty(clientHashSequence[1])) return false;
            try
            {
                var serverTime = Convert.ToDateTime(clientHashSequence[1]);
                if (Math.Abs((serverTime - DateTime.Now).Minutes) <= 1 &&
                    Math.Abs((serverTime - DateTime.Now).Minutes) >= 0 &&
                    Sha1Managed.ValidateSha1HashData(compareAuthHash, authHash))
                    result = true;
            }
            catch (Exception)
            {
                //DA FARE
                //IMPLEMENTA UN SISTEMA DI LOG (UNA LIBRERIA DI CLASSI CONDIVISA)
            }
            return result;
        }
    }
}