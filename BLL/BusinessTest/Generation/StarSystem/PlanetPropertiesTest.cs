using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Generation.StarSystem;

namespace BusinessTest.Generation.StarSystem
{
    [TestClass]
    public class PlanetPropertiesTest
    {
        private static Random _rnd;
        public PlanetPropertiesTest()
        {
            _rnd = new Random();
        }
        [TestMethod]
        public void TestCalculateSpaces()
        {
            //piccole radiazioni con atmosfera
            var x = PlanetProperties.CalculateSpaces(100, 6,
                BLL.Generation.StarSystem.IstanceFactory.FactoryGenerator.RetrieveSystemGenerationDto(
                    false, false, false, false, false, false, false, 0, 0, 0, 0), true, true, _rnd);

            Assert.IsTrue(x.HabitableSpaces < x.Totalspaces);
            Assert.IsTrue(x.Totalspaces == 100);

            //Medio alte radiazioni, senza acqua, senza atmosfera
            x = PlanetProperties.CalculateSpaces(100, 8, BLL.Generation.StarSystem.IstanceFactory.FactoryGenerator.RetrieveSystemGenerationDto(
                false, false, false, false, false, false, false, 0, 0, 0, 0), false, false, _rnd);
            Assert.IsTrue(x.WaterSpaces == 0);
            Assert.IsTrue(x.Totalspaces == 100);
        }
    }
}
