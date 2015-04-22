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
        
        public string GenerateStarSystem(PlanetGenerationDto generationData)
        {
            IntRange RangeX = new IntRange(generationData.MinX, generationData.MaxX);
            IntRange RangeY = new IntRange(generationData.MinY, generationData.MaxY);
            bool generationResult = false;
            string result = "Generation ended with: ";
            using (ContextFactory factory = new ContextFactory())
            {
                IContext context = factory.Retrieve();
                UowRepositories repositories = factory.CreateRepositories();
                DalCache cache = factory.CreateCache();
                using (UowRepositoryFactories repoFactory = new UowRepositoryFactories(context,cache,repositories))
                {
                    using (MainUow uow = new MainUow(context,cache,repoFactory))
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

            return result + ((generationResult) ? " OK status" : " KO status");
        }

        public List<StarDto> GetUniversePortion(UniverseRangeDto universeRage)
        {
            IntRange rangeX = new IntRange(universeRage.MinX, universeRage.MaxX);
            IntRange rangeY = new IntRange(universeRage.MinY, universeRage.MaxY);
            List<StarDto> stars = new List<StarDto>();
            List<Star> starEntities = new List<Star>();
            StarEntityMapper mapper = new StarEntityMapper();
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
                        string cacheKey = rangeX.Min+"_x_"+rangeX.Max+";"+rangeY.Min+"_y_"+rangeY.Max;                        
                        starEntities = Retrieve.StarsInRange(cacheKey);
                        
                    }
                }
            }
            if (starEntities!=null)
                stars = mapper.EntityListToModel(starEntities);
            return stars;
        }
    }
}
