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
            Assert.IsTrue(StarProperties.DetermineStarColor(50) == StarColor.Red);
            Assert.IsTrue(StarProperties.DetermineStarColor(98) == StarColor.Blue);
            Assert.IsTrue(StarProperties.DetermineStarColor(92) == StarColor.Orange);
            Assert.IsTrue(StarProperties.DetermineStarColor(96) == StarColor.White);
            Assert.IsTrue(StarProperties.DetermineStarColor(51) == StarColor.Yellow);
        }

        [TestMethod]
        public void DetermineStarTypeTest()
        {
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange, 12) == StarType.Dwarf);
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange, 87) == StarType.Giant);
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange, 100) == StarType.HyperGiant);
        }

        [TestMethod]
        public void TestDetermineStarRadiationBlue()
        {
            Assert.AreEqual(14, StarProperties.DetermineStarRadiation(StarColor.Blue, 8));
            Assert.AreEqual(10, StarProperties.DetermineStarRadiation(StarColor.Blue, 0));
            Assert.AreEqual(15, StarProperties.DetermineStarRadiation(StarColor.Blue, 10));
        }

        [TestMethod]
        public void TestDetermineSurfaceTemp()
        {
            Assert.AreEqual(3450, StarProperties.DetermineSurfaceTemp(StarColor.Red, StarType.HyperGiant, 5));
            Assert.AreEqual(40000, StarProperties.DetermineSurfaceTemp(StarColor.Blue, StarType.HyperGiant, 10));
        }

        [TestMethod]
        public void TestDetermineStarMass()
        {
            Assert.AreEqual(150, StarProperties.DetermineStarMass(StarType.Dwarf, StarColor.Blue, 10));
        }
    }
}