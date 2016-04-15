using System;
using System.Configuration;
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
            const int maxX = 248;
            const int maxY = 40;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX,maxY)==SectorRegion.Centre);
        }

        [TestMethod]
        public void ShouldBeInAverage()
        {
            const int maxX = 310;
            const int maxY = 40;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX, maxY) == SectorRegion.Average);
        }

        [TestMethod]
        public void ShouldBeJustOutside()
        {
            const int maxX = 20;
            const int maxY = 540;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX, maxY) == SectorRegion.JustOutside);
        }

        [TestMethod]
        public void ShouldBeFarAway()
        {
            const int maxX = 230;
            const int maxY = 1400;
            Assert.IsTrue(SectorProperties.WhereAmI(maxX, maxY) == SectorRegion.FarAway);
        }

        [TestMethod]
        public void ShouldHaveALotOfStars()
        {
            int max = Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionA"]);
            Assert.IsTrue(max==SectorProperties.RetrieveMaxNumberOfStars(SectorRegion.Centre));
        }

        [TestMethod]
        public void ShouldHaveANotSoMuchOfStars()
        {
            int max = Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionB"]);
            Assert.IsTrue(max == SectorProperties.RetrieveMaxNumberOfStars(SectorRegion.Average));
        }

        [TestMethod]
        public void ShouldHaveABitLessOfStars()
        {
            int max = Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionC"]);
            Assert.IsTrue(max == SectorProperties.RetrieveMaxNumberOfStars(SectorRegion.JustOutside));
        }

        [TestMethod]
        public void ShouldHaveALesserOfStars()
        {
            int max = Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionD"]);
            Assert.IsTrue(max == SectorProperties.RetrieveMaxNumberOfStars(SectorRegion.FarAway));
        }
    }
}
