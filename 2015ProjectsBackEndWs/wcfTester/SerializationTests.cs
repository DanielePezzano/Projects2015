using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using SharedDto.Universe.Planets;
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
                Planets = new List<PlanetDto>()
            };

            //var star = new Star
            //{
            //    Name = "testStar",
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now,
            //    Galaxy = new Galaxy {Name = "TestGalaxy", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
            //    Planets = new List<Planet>()
            //};

            //var planet = new Planet
            //{
            //    Star = star,
            //    Name = "PLanettest",
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now
            //};

            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(Star));

            ser.WriteObject(stream, starDto);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();

            Assert.IsTrue(string.IsNullOrEmpty(result) == false);
        }
    }
}
