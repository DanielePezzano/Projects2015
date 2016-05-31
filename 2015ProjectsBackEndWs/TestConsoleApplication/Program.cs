using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using BLL.Generation.Sector;
using DAL.Mappers.Universe;
using DAL.Mappers.Universe.Enums;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Universe;
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
                MaxX = 100,
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
            if (result.GeneratedStarsList == null || result.GeneratedStarsList.Count <= 0) return;
            var resultDto = generatore.SaveResult(result.GeneratedStarsList);
            //var factory = IstancesCreator.RetrieveOpFactory("UniverseConnection");
            //var opresult = factory.SetOperation(MappedRepositories.StarRepository, MappedOperations.GetAll, "getAllGalaxies", null);
            //var mapper = MapperFactory.RetrieveMapper(factory, UniverseMapperTypes.stars);
            //var resultDto = ((StarMapper)mapper).EntityListToModel((List<Star>)opresult.RawResult);
            //if (resultDto.Count>0)
            //{

            //    var stream = new FileStream(@"C:\Temp\starSerialized.txt", FileMode.Append);
            //    var ser = new DataContractJsonSerializer(typeof(StarDto));

            //    ser.WriteObject(stream, resultDto.First()); 
            //}


            Console.ReadLine();
        }
    }
}
