using System;
using System.Configuration;
using System.Web.Script.Serialization;
using SharedDto;
using SharedDto.Form.Account;
using SharedDto.Form.Universe;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;
using WcfCommCrypto;
using _2015ProjectsBackEndWs.Security;
using _2015ProjectsBackEndWs.ServiceLogic;
using _2015ProjectsBackEndWs.Utility;

namespace _2015ProjectsBackEndWs
{
    public class Universe : IUniverse
    {
        private static Random _rnd;

        public Universe()
        {
            if (_rnd == null) _rnd = new Random();
        }

        /// <summary>
        ///     this must be called only once per user, every 30 minutes
        /// </summary>
        /// <param name="generationData"></param>
        /// <param name="hashcall"></param>
        /// <returns></returns>
        public string GenerateStarSystem(SystemGenerationDto generationData, string hashcall)
        {
            if (hashcall != null)
            {
                var decriptedHash = RijndaelManagedEncryption.DecryptRijndael(hashcall,
                    ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                    ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
                if (decriptedHash.StartsWith(CallStartSentences.GenerateStarSystem))
                {
                    if (!RepetitionChecker.Check(hashcall))
                    {
                        var generationResult = ProcessStarSystemGeneration(generationData);
                        if (generationResult) return CallsStatusResponse.GenericCallSuccess;
                    }
                    else
                    {
                        _rnd = null;
                        return CallsStatusResponse.GenericWait;
                    }
                }
            }
            _rnd = null;
            return CallsStatusResponse.GenericCallFailed;
        }

        /// <summary>
        ///     Json Retrieve Method
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RetrieveUniversePortion(string data)
        {
            var result = CallsStatusResponse.GenericCallFailed;
            if (string.IsNullOrEmpty(data)) return result;
            try
            {
                //Decriptalo con la nostra chiave
                var decriptedHash = RijndaelManagedEncryption.DecryptRijndael(data,
                    ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                    ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
                var javascriptSerializer = new JavaScriptSerializer();
                var universeRange = javascriptSerializer.Deserialize<UniverseRangeDto>(decriptedHash);
                //is correctly deserialized and it was sent in time
                if (universeRange != null &&
                    Validation.Validate(universeRange.Auth, CallInstanceName.UniverseRangeDto))
                {
                    using (var getter = new GetOnly())
                    {
                        var starEntities = getter.ProcessRetrieveMethod(universeRange);
                        using (var serializator = new ProcessSerialization())
                        {
                            result = serializator.SerializeJson(typeof (SectorDto),
                                new SectorDto {Stars = starEntities});
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Da FAre: Sistema di gestione log di errore
            }
            return result;
        }

        /// <summary>
        ///     Questo metodo restituisce il tempo corrente del servizio: usalo con il client prima di ogni
        ///     successiva chiamata: dal momento che il server ti da il tempo, hai un minuto per creare
        ///     dal client, una chiamata valida.
        /// </summary>
        /// <returns></returns>
        public string ServiceTime()
        {
            string result;
            using (var serializator = new ProcessSerialization())
            {
                result = serializator.SerializeJson(typeof (DateTime), DateTime.Now);
            }
            return result;
        }

        /// <summary>
        ///     Ritorna tutte le informazioni utili di un pianeta
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RetrievePlanetInfo(string data)
        {
            var result = CallsStatusResponse.GenericCallFailed;
            if (string.IsNullOrEmpty(data)) return result;
            //Decriptalo con la nostra chiave
            var decriptedHash = RijndaelManagedEncryption.DecryptRijndael(data,
                ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
            var javascriptSerializer = new JavaScriptSerializer();
            var info = javascriptSerializer.Deserialize<RetrievingInfoDto>(decriptedHash);
            //is correctly deserialized and it was sent in time
            if (info == null || !Validation.Validate(info.Auth, CallInstanceName.RetrievingInfoDto)) return result;
            using (var getter = new GetOnly())
            {
                var planet = getter.RetrieveSinglePlanet(info.Id);
                using (var serializator = new ProcessSerialization())
                {
                    result = serializator.SerializeJson(typeof (PlanetDto), planet);
                }
            }
            return result;
        }

        /// <summary>
        ///     Quando un utente cerca di registrarsi manda un form al back-end
        ///     questo controlla che esista una email equivalente e se esiste
        ///     gli risponde con un negativo
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CheckUserRegistration(string data)
        {
            bool result;
            var decriptedHash = RijndaelManagedEncryption.DecryptRijndael(data,
                ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                ConfigurationManager.AppSettings[ConfAppSettings.InputKey]);
            var javascriptSerializer = new JavaScriptSerializer();
            var item = javascriptSerializer.Deserialize<RegisterModel>(decriptedHash);

            using (var getter = new GetOnly())
            {
                result = getter.IsEmailFree(item.Email);
            }
            return result;
        }

        /// <summary>
        ///     Get the universe List to use in a dropdownlist
        /// </summary>
        /// <returns></returns>
        public string RetrieveUniverseList()
        {
            string result;
            using (var getter = new GetOnly())
            {
                var galaxyList = getter.RetrieveGalaxyList();
                using (var serializer = new ProcessSerialization())
                {
                    result = serializer.SerializeJson(typeof (GalaxyList), galaxyList);
                }
            }
            return result;
        }

        #region Private Methods

        /// <summary>
        ///     If all Checks goes well, the system is created
        /// </summary>
        /// <param name="generationData"></param>
        private static bool ProcessStarSystemGeneration(SystemGenerationDto generationData)
        {
            bool generationResult;
            using (var setter = new SetOnly())
            {
                generationResult = setter.GenerateStarSystem(generationData, _rnd);
            }
            return generationResult;
        }

        #endregion
    }
}