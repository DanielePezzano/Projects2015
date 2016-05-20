using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Models.Races.Enums;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Stars;

namespace wcfTester
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void TestJsonStarDtoSerialization()
        {
            var starDto = new StarDto
            {
                Name = "testStar",
                GalaxyId = 1,
                Planets = new List<PlanetDto>(),
                CreatedAt = DateTime.Now
            };
            
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(StarDto));

            ser.WriteObject(stream, starDto);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();

            Assert.IsTrue(string.IsNullOrEmpty(result) == false);
        }

        [TestMethod]
        public void ShouldSerializeRaceDto()
        {
            var raceDto = new RaceDto()
            {
                RaceName = "RaceTest",
                RacePointsLeft = 0,
                RacePointsUsed = 10,
                RaceBonuses = new List<RaceBonusDto>()
            };

            var bonusA = new RaceBonusDto()
            {
                Bonus = RaceTraitsBonuses.OreProduction,
                TraitType = RaceTraitsType.Social,
                Value = 30,
                CreatedAt = DateTime.Now
            };

            var bonusB = new RaceBonusDto()
            {
                Bonus = RaceTraitsBonuses.FoodConsumption,
                TraitType = RaceTraitsType.Cultural,
                Value = -20,
                CreatedAt = DateTime.Now
            };

            raceDto.RaceBonuses.Add(bonusA);
            raceDto.RaceBonuses.Add(bonusB);


            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(RaceDto));

            ser.WriteObject(stream, raceDto);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();

            Assert.IsTrue(string.IsNullOrEmpty(result) == false);
        }
    }
}
