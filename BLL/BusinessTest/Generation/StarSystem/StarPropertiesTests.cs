using BLL.Generation.StarSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe.Enum;

namespace BusinessTest.Generation.StarSystem
{
    [TestClass]
    public class StarPropertiesTests
    {
        [TestMethod]
        public void DetermineStarColorTest()
        {
            Assert.IsTrue(StarProperties.DetermineStarColor(50) == StarColor.Red.ToString());
            Assert.IsTrue(StarProperties.DetermineStarColor(98) == StarColor.Blue.ToString());
            Assert.IsTrue(StarProperties.DetermineStarColor(92) == StarColor.Orange.ToString());
            Assert.IsTrue(StarProperties.DetermineStarColor(96) == StarColor.White.ToString());
            Assert.IsTrue(StarProperties.DetermineStarColor(51) == StarColor.Yellow.ToString());
        }

        [TestMethod]
        public void DetermineStarTypeTest()
        {
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange.ToString(), 12) == StarType.Dwarf.ToString());
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange.ToString(), 87) == StarType.Giant.ToString());
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange.ToString(), 100) == StarType.HyperGiant.ToString());
        }

        [TestMethod]
        public void TestDetermineStarRadiationBlue()
        {
            Assert.AreEqual(14, StarProperties.DetermineStarRadiation(StarColor.Blue.ToString(), 8));
            Assert.AreEqual(10, StarProperties.DetermineStarRadiation(StarColor.Blue.ToString(), 0));
            Assert.AreEqual(15, StarProperties.DetermineStarRadiation(StarColor.Blue.ToString(), 10));
        }

        [TestMethod]
        public void TestDetermineSurfaceTemp()
        {
            Assert.AreEqual(3450, StarProperties.DetermineSurfaceTemp(StarColor.Red.ToString(), StarType.HyperGiant.ToString(), 5));
            Assert.AreEqual(40000, StarProperties.DetermineSurfaceTemp(StarColor.Blue.ToString(), StarType.HyperGiant.ToString(), 10));
        }

        [TestMethod]
        public void TestDetermineStarMass()
        {
            Assert.AreEqual(150, StarProperties.DetermineStarMass(StarType.Dwarf.ToString(), StarColor.Blue.ToString(), 10));
        }
    }
}