using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2015ProjectsBackEndWs.DTO.Universe;
using Moq;
using Models.Universe;
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
            Star star = new Star();
            star.Name = "testStar";
            star.CreatedAt = DateTime.Now;
            star.UpdatedAt = DateTime.Now;
            star.Galaxy = new Galaxy() { Name= "TestGalaxy", CreatedAt= DateTime.Now, UpdatedAt = DateTime.Now};
            star.Planets = new List<Planet>();

            Planet planet = new Planet();
            planet.Star = star;
            planet.Name = "PLanettest";
            planet.CreatedAt = DateTime.Now;
            planet.UpdatedAt = DateTime.Now;

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Star));

            ser.WriteObject(stream, star);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            Assert.IsTrue(string.IsNullOrEmpty(result) == false);
        }
    }
}
