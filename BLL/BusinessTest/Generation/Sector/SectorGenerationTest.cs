using System;
using BLL.Generation.Sector.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedDto.Universe.Sector;

namespace BusinessTest.Generation.Sector
{
    [TestClass]
    public class SectorGenerationTest
    {
        [TestMethod]
        public void TestShouldReturn()
        {
            var generator = FactoryGenerator.RetrieveGenerateSector(20, 0, 40, 0, 50, null, new Random(),1);
            var result = generator.Generate(true);
            Assert.IsInstanceOfType(result,typeof(SectorGenerationDto));
            Assert.AreEqual(0,result.StarsGenerated);
            Assert.AreEqual(SectorGenerationResult.MaxStarReached, result.GenerationResult);
        }
    }
}
