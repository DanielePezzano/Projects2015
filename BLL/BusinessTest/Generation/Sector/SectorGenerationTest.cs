using System;
using BLL.Generation.Sector.IstanceFactory;
using DAL.Operations.IstanceFactory;
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
            var generator = FactoryGenerator.RetrieveGenerateSector(20, new Random(),
                BLL.Generation.StarSystem.IstanceFactory.FactoryGenerator.RetrieveSystemGenerationDto(
                    false, false, false, false, false, false, false, 0, 40, 0, 50), IstancesCreator.RetrieveOpFactory("UniverseConnection", true));
            
            var result = generator.Generate(true);
            Assert.IsInstanceOfType(result,typeof(SectorGenerationDto));
            Assert.AreEqual(0,result.StarsGenerated);
            Assert.AreEqual(SectorGenerationResult.MaxStarReached, result.GenerationResult);
        }
    }
}
