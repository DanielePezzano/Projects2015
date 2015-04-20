using _2015ProjectsBackEndWs.DTO.UtilityDto;
using BLL.Utilities.Structs;
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

        public string GetUniversePortion(string minX, string minY, string maxX, string maxY)
        {
            throw new NotImplementedException();
        }

        public string GeneratePortion(PlanetGenerationDto generationData)
        {
            IntRange RangeX = new IntRange(generationData.MinX, generationData.MaxX);
            IntRange RangeY = new IntRange(generationData.MinY, generationData.MaxY);
            bool generationResult = false;
            string result = "Generation ended with: ";
            using (ContextFactory factory = new ContextFactory())
            {
                using (MainUow uow = new MainUow(factory, new UnitOfWork.Cache.DalCache()))
                {
                    BLL.Generation.GeneratePortion generator = new BLL.Generation.GeneratePortion(
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

            return result + ((generationResult) ? " OK status" : " KO status");
        }
    }
}
