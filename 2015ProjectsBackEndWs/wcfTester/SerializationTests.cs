using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace wcfTester
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void TestJsonStarDtoSerialization()
        {
            var star = new Star
            {
                Name = "testStar",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Galaxy = new Galaxy() {Name = "TestGalaxy", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                Planets = new List<Planet>()
            };

            //var planet = new Planet
            //{
            //    Star = star,
            //    Name = "PLanettest",
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now
            //};

            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(Star));

            ser.WriteObject(stream, star);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();

            Assert.IsTrue(string.IsNullOrEmpty(result) == false);
        }
    }
}
