using _2015ProjectsBackEndWs.DTO.UtilityDto;
using BLL.Utilities.Structs;
using System;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Implementations.Uows.UowDto;
using BLL.Generation;
using BLL.Information;
using System.Collections.Generic;
using Models.Universe;
using _2015ProjectsBackEndWs.DataMapper;
using _2015ProjectsBackEndWs.DTO.Universe;
using _2015ProjectsBackEndWs.Utility;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Web.Script.Serialization;
using WcfCommCrypto;
using _2015ProjectsBackEndWs.Security;

namespace _2015ProjectsBackEndWs
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Universe" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Universe.svc or Universe.svc.cs at the Solution Explorer and start debugging.

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

        /// <summary>
        /// If all Checks goes well, the system is created
        /// </summary>
        /// <param name="generationData"></param>
        /// <param name="generationResult"></param>
        /// <param name="result"></param>
        private bool ProcessStarSystemGeneration(PlanetGenerationDto generationData)
        {
            IntRange RangeX = new IntRange(generationData.MinX, generationData.MaxX);
            IntRange RangeY = new IntRange(generationData.MinY, generationData.MaxY);
            bool generationResult = false;
            using (ContextFactory factory = new ContextFactory())
            {
                IContext context = factory.Retrieve();
                UowRepositories repositories = factory.CreateRepositories();
                DalCache cache = factory.CreateCache();
                using (UowRepositoryFactories repoFactory = new UowRepositoryFactories(context, cache, repositories))
                {
                    using (MainUow uow = new MainUow(context, cache, repoFactory))
                    {
                        GeneratePortion generator = new GeneratePortion(
                            RangeX.Min,
                            RangeX.Max,
                            RangeY.Min,
                            RangeY.Max,
                            uow,
                            generationData.ForceLiving,
                            generationData.ForceWater,
                            generationData.MostlyWater,
                            generationData.MineralPoor,
                            generationData.MineralRich,
                            generationData.FoodPoor,
                            generationData.FoodRich);

                        generationResult = generator.Generate(_Rnd);
                    }
                }
            }
            return generationResult;
        }
        
        private List<StarDto> ProcessRetrieveMethod(UniverseRangeDto universeRage)
        {
            IntRange rangeX = new IntRange(universeRage.MinX, universeRage.MaxX);
            IntRange rangeY = new IntRange(universeRage.MinY, universeRage.MaxY);
            List<StarDto> stars = new List<StarDto>();
            StarEntityMapper mapper = new StarEntityMapper();

            List<Star> starEntities = RetrieveInformation(ref rangeX, ref rangeY);
            if (starEntities != null)
                stars = mapper.EntityListToModel(starEntities);
            return stars;
        }

        private List<Star> RetrieveInformation(ref IntRange rangeX, ref IntRange rangeY)
        {
            List<Star> starEntities = new List<Star>();
            using (ContextFactory factory = new ContextFactory())
            {
                IContext context = factory.Retrieve();
                UowRepositories repositories = factory.CreateRepositories();
                DalCache cache = factory.CreateCache();
                using (UowRepositoryFactories repoFactory = new UowRepositoryFactories(context, cache, repositories))
                {
                    using (MainUow uow = new MainUow(context, cache, repoFactory))
                    {
                        RetrieveInformations Retrieve = new RetrieveInformations(uow, rangeX, rangeY);
                        string cacheKey = rangeX.Min + CallSeparators.coordX + rangeX.Max + CallSeparators.otherSeparator + rangeY.Min + CallSeparators.coordY + rangeY.Max;
                        starEntities = Retrieve.StarsInRange(cacheKey);

                    }
                }
            }
            return starEntities;
        }
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
                    if (universeRange != null && 
                        ValidateCall.Validate(
                        universeRange.Auth.GeneratedStamp, 
                        universeRange.Auth.AuthHash_01,
                        universeRange.Auth.AuthHash_02,
                        ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                        ConfigurationManager.AppSettings[ConfAppSettings.InputKey],
                        CallInstanceName.UniverseRangeDto,
                        ConfigurationManager.AppSettings[ConfAppSettings.Username],
                        ConfigurationManager.AppSettings[ConfAppSettings.Password],
                        AcceptedClients.WhiteList,
                        CallSeparators.defaultSeparator))
                    {
                        List<StarDto> starEntities = ProcessRetrieveMethod(universeRange);
                        using (ProcessSerialization serializator = new ProcessSerialization())
                        {
                            result = serializator.SerializeJson(typeof(SectorDto), new SectorDto() { Stars = starEntities });
                        }
                    }
                }
                catch (Exception ex)
                {
                    
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
                result = serializator.SerializeJson(typeof(DateTime),DateTime.Now);
            }
            return result;
        }


        public string RetrieveUniversePortionUnused(UniverseRangeDto data)
        {
            return CallsStatusResponse.GenericCallSuccess;
        }
    }
}
