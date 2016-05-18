using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Generation.Sector;
using DAL.Operations.IstanceFactory;
using SharedDto.UtilityDto;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var generationDto = new SystemGenerationDto()
            {
             MaxX   = 100,
             MinX = 50,
             ForceWater = true,
             MinY = 300,
             MaxY = 500,
             MostlyWater = true,
             MineralRich = false,
             ForceLiving = true,
             MineralPoor = false,
             GalaxyId = 1,
             FoodPoor = false,
             FoodRich = true
            };
            var generatore = new GenerateSector(0, new Random(), generationDto, IstancesCreator.RetrieveOpFactory("UniverseConnection"));

            var result = generatore.Generate();
            if (result.GeneratedStarsList != null && result.GeneratedStarsList.Count > 0)
            {
                generatore.SaveResult(result.GeneratedStarsList);
            }
        }
    }
}
