using _2015ProjectsBackEndWs.Security;
using _2015ProjectsBackEndWs.ServiceLogic;
using _2015ProjectsBackEndWs.Utility;
using SharedDto;
using SharedDto.Form.Account;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using WcfCommCrypto;

namespace _2015ProjectsBackEndWs
{
    public class Universe : IUniverse
    {
        private static Random _Rnd;

        public Universe()
        {
            if (_Rnd == null) _Rnd = new Random();
        }
        /// <summary>
        /// this must be called only once per user, every 30 minutes
        /// </summary>
        /// <param name="generationData"></param>
        /// <param name="hashcall"></param>
        /// <returns></returns>
        public string GenerateStarSystem(PlanetGenerationDto generationData, string hashcall)
        {
            if (hashcall != null)
            {
                string decriptedHash = RijndaelManagedEncryption.DecryptRijndael(hashcall, ConfigurationManager.AppSettings[ConfAppSettings.SaltKey], ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
                if (decriptedHash.StartsWith(CallStartSentences.GeneratePortion))
                {
                    if (!RepetitionChecker.Check(hashcall))
                    {
                        bool generationResult = ProcessStarSystemGeneration(generationData);
                        if (generationResult) return CallsStatusResponse.GenericCallSuccess;
                    }
                    else
                    {
                        _Rnd = null;
                        return CallsStatusResponse.GenericWait;
                    }
                }
            }
            _Rnd = null;
            return CallsStatusResponse.GenericCallFailed;
        }

        #region Private Methods
        /// <summary>
        /// If all Checks goes well, the system is created
        /// </summary>
        /// <param name="generationData"></param>
        /// <param name="generationResult"></param>
        /// <param name="result"></param>
        private bool ProcessStarSystemGeneration(PlanetGenerationDto generationData)
        {
            bool generationResult = false;
            using (SetOnly setter = new SetOnly())
            {
                generationResult = setter.GenerateStarSystem(generationData, _Rnd);
            }            
            return generationResult;
        }
        
        #endregion
        /// <summary>
        /// Json Retrieve Method
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RetrieveUniversePortion(string data)
        {
            string result = CallsStatusResponse.GenericCallFailed;
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    //Decriptalo con la nostra chiave
                    string decriptedHash = RijndaelManagedEncryption.DecryptRijndael(data, ConfigurationManager.AppSettings[ConfAppSettings.SaltKey], ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
                    var javascriptSerializer = new JavaScriptSerializer();
                    UniverseRangeDto universeRange = javascriptSerializer.Deserialize<UniverseRangeDto>(decriptedHash);
                    //is correctly deserialized and it was sent in time
                    if (universeRange != null && Validation.Validate(universeRange.Auth, CallInstanceName.UniverseRangeDto))
                    {
                        List<StarDto> starEntities = new List<StarDto>();
                        using (GetOnly getter = new GetOnly())
                        {
                            starEntities = getter.ProcessRetrieveMethod(universeRange);
                            using (ProcessSerialization serializator = new ProcessSerialization())
                            {
                                result = serializator.SerializeJson(typeof(SectorDto), new SectorDto() { Stars = starEntities });
                            } 
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Da FAre: Sistema di gestione log di errore
                }
            }
            return result;
        }
        /// <summary>
        /// Questo metodo restituisce il tempo corrente del servizio: usalo con il client prima di ogni 
        /// successiva chiamata: dal momento che il server ti da il tempo, hai un minuto per creare
        /// dal client, una chiamata valida.
        /// 
        /// </summary>
        /// <returns></returns>
        public string ServiceTime()
        {
            string result = string.Empty;
            using (ProcessSerialization serializator = new ProcessSerialization())
            {
                result = serializator.SerializeJson(typeof(DateTime), DateTime.Now);
            }
            return result;
        }
        /// <summary>
        /// Ritorna tutte le informazioni utili di un pianeta
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RetrievePlanetInfo(string data)
        {
            string result = CallsStatusResponse.GenericCallFailed;
            if (!string.IsNullOrEmpty(data))
            {
                //Decriptalo con la nostra chiave
                string decriptedHash = RijndaelManagedEncryption.DecryptRijndael(data, ConfigurationManager.AppSettings[ConfAppSettings.SaltKey], ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
                var javascriptSerializer = new JavaScriptSerializer();
                RetrievingInfoDto info = javascriptSerializer.Deserialize<RetrievingInfoDto>(decriptedHash);
                //is correctly deserialized and it was sent in time
                if (info != null && Validation.Validate(info.Auth,CallInstanceName.RetrievingInfoDto))
                {
                    PlanetDto planet = null;
                    using (GetOnly getter = new GetOnly())
                    {
                        planet = getter.RetrieveSinglePlanet(info.Id);
                        using (ProcessSerialization serializator = new ProcessSerialization())
                        {
                            result = serializator.SerializeJson(typeof(PlanetDto), planet);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Quando un utente cerca di registrarsi manda un form al back-end
        /// questo controlla che esista una email equivalente e se esiste
        /// gli risponde con un negativo
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CheckUserRegistration(string data)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(data))
            {
                string decriptedHash = RijndaelManagedEncryption.DecryptRijndael(data, ConfigurationManager.AppSettings[ConfAppSettings.SaltKey], ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
                var javascriptSerializer = new JavaScriptSerializer();
                RegisterModel item = javascriptSerializer.Deserialize<RegisterModel>(decriptedHash);
                if (item != null)
                {
                    using (GetOnly getter = new GetOnly())
                    {
                        result = getter.IsEmailFree(item.Email);
                    }
                }
            }
            return result;
        }
    }
}
