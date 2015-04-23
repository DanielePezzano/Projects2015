using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfTester.UniverseWcf;
using WcfTester.Utility;
namespace WcfTester
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanetGenerationDto dto = new PlanetGenerationDto();
            string result = string.Empty;
            dto.FoodPoor = false;
            dto.FoodRich = false;
            dto.ForceLiving = false;
            dto.ForceWater = false;
            dto.MineralPoor = false;
            dto.MineralRich = false;
            dto.MostlyWater = false;

            dto.MinX = 50;
            dto.MaxX = 100;
            dto.MinY = 50;
            dto.MaxY = 100;

            using (UniverseClient client = new UniverseClient())
            {
                string hashCall = RijndaelManagedEncryption.EncryptRijndael(CallStartSentences.GeneratePortion + "123_ MinX = 50, MaxX = 100, MinY = 50, MaxY = 100");
                result = client.GenerateStarSystem(dto, hashCall);
            }


            
            //using (UniverseClient client = new UniverseClient())
            //{

                
            //    _2015ProjectsBackEndWsDTOUniverseStarDto[] stars = client.GetUniversePortion(new UniverseRangeDto() { MinX = 30, MaxX = 200, MinY = 0, MaxY = 100 }, hashCall);
            //}
            Console.Write(result);
            Console.Read();
        }
    }
}
