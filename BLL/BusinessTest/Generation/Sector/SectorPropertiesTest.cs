using System;
using BLL.Generation.Sector;
using BLL.Generation.Sector.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest.Generation.Sector
{
    [TestClass]
    public class SectorPropertiesTest
    {
        [TestMethod]
        public void ShouldBeInCentre()
        {
            var maxX = 248;
            var maxY = 40;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX,maxY)==SectorRegion.Centre);
        }

        [TestMethod]
        public void ShouldBeInAverage()
        {
            var maxX = 310;
            var maxY = 40;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX, maxY) == SectorRegion.Average);
        }

        [TestMethod]
        public void ShouldBeJustOutside()
        {
            var maxX = 20;
            var maxY = 540;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX, maxY) == SectorRegion.JustOutside);
        }

        [TestMethod]
        public void ShouldBeFarAway()
        {
            var maxX = 230;
            var maxY = 1400;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX, maxY) == SectorRegion.FarAway);
        }
    }
}
