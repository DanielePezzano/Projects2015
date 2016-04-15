using BLL.Generation.StarSystem.IstanceFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using SharedDto.Universe.Stars;

namespace BusinessTest.Generation.StarSystem
{
    [TestClass]
    public class StarGenerationTests
    {
        [TestMethod]
        public void CreateBrandNewStarTest()
        {
            var generator = FactoryGenerator.RetrieveStarBuilder();
            Assert.IsInstanceOfType(generator.CreateBrandNewStar(), typeof(StarDto));
        }
    }
}
