using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Generation.StarSystem;
using Models.Base;

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
            Spaces x = PlanetProperties.CalculateSpaces(100, 6, new PlanetCustomConditions(), true, true, _rnd);

            Assert.IsTrue(x.HabitableSpaces < x.Totalspaces);
            Assert.IsTrue(x.Totalspaces == 100);

            //Medio alte radiazioni, senza acqua, senza atmosfera
            x = PlanetProperties.CalculateSpaces(100, 8, new PlanetCustomConditions(), false, false, _rnd);
            Assert.IsTrue(x.WaterSpaces == 0);
            Assert.IsTrue(x.Totalspaces == 100);
        }
    }
}
