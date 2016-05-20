using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using BLL.Generation.Sector;
using DAL.Operations.IstanceFactory;
using SharedDto.Universe.Stars;
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
             FoodRich = true,
             IsHomePlanet = true
            };
            var generatore = new GenerateSector(0, new Random(), generationDto, IstancesCreator.RetrieveOpFactory("UniverseConnection"));

            var result = generatore.Generate();
            if (result.GeneratedStarsList != null && result.GeneratedStarsList.Count > 0)
            {
                var resultDto = generatore.SaveResult(result.GeneratedStarsList);
                var stream = new MemoryStream();
                var ser = new DataContractJsonSerializer(typeof(StarDto));

                ser.WriteObject(stream, resultDto);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                Console.WriteLine(sr.ReadToEnd());
                Console.ReadLine();
            }
        }
    }
}
