using _2015ProjectsBackEndWs.DTO.UtilityDto;
using System;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;

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

        public string GetUniversePortion(int minX, int minY, int maxX, int maxY)
        {
            throw new NotImplementedException();
        }
        
        public void GeneratePortion(PlanetGenerationDto generationData)
        {
            using (ContextFactory factory = new ContextFactory())
            {
                using (MainUow uow = new MainUow(factory, new UnitOfWork.Cache.DalCache()))
                {
                    BLL.Generation.GeneratePortion generator = new BLL.Generation.GeneratePortion(
                        generationData.RangeX.Min,
                        generationData.RangeX.Max,
                        generationData.RangeY.Min,
                        generationData.RangeY.Max,
                        uow,
                        generationData.ForceLiving,
                        generationData.ForceWater,
                        generationData.MostlyWater,
                        generationData.MineralPoor,
                        generationData.MineralRich,
                        generationData.FoodPoor,
                        generationData.FoodRich);

                    generator.Generate(_Rnd);
                }
            }
        }
    }
}
