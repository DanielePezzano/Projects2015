using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfTester.UniverseWcf;
namespace WcfTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlanetGenerationDto dto = new PlanetGenerationDto();
            //dto.FoodPoor = false;
            //dto.FoodRich = false;
            //dto.ForceLiving = false;
            //dto.ForceWater = false;
            //dto.MineralPoor = false;
            //dto.MineralRich = false;
            //dto.MostlyWater = false;

            //dto.MinX = 50;
            //dto.MaxX = 100;
            //dto.MinY = 50;
            //dto.MaxY = 100;

            //using (UniverseClient client = new UniverseClient())
            //{
            //    client.GenerateStarSystem(dto);
            //}
            string result = string.Empty;
            using (UniverseClient client = new UniverseClient())
            {
                
                _2015ProjectsBackEndWsDTOUniverseStarDto[] stars = client.GetUniversePortion(new UniverseRangeDto() { MinX = 30, MaxX = 200, MinY = 0, MaxY = 100 });
            }
            Console.Write(result);
            Console.Read();
        }
    }
}
