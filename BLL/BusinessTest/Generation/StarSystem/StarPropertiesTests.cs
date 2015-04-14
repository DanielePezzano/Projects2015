﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Generation.StarSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe.Enum;
namespace BLL.Generation.StarSystem.Tests
{
    [TestClass()]
    public class StarPropertiesTests
    {
        [TestMethod()]
        public void DetermineStarColorTest()
        {
            Assert.IsTrue(StarProperties.DetermineStarColor(50) == StarColor.Red);
            Assert.IsTrue(StarProperties.DetermineStarColor(98) == Models.Universe.Enum.StarColor.Blue);
            Assert.IsTrue(StarProperties.DetermineStarColor(92) == Models.Universe.Enum.StarColor.Orange);
            Assert.IsTrue(StarProperties.DetermineStarColor(96) == Models.Universe.Enum.StarColor.White);
            Assert.IsTrue(StarProperties.DetermineStarColor(51) == Models.Universe.Enum.StarColor.Yellow);
        }

        [TestMethod()]
        public void DetermineStarTypeTest()
        {
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange,12) == Models.Universe.Enum.StarType.Dwarf);
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange, 87) == Models.Universe.Enum.StarType.Giant);
            Assert.IsTrue(StarProperties.DetermineStarType(StarColor.Orange, 100) == Models.Universe.Enum.StarType.HyperGiant);
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
    }
}