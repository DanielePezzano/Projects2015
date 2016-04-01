using BLL.Generation.StarSystem.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;

namespace BusinessTest.Generation.StarSystem
{
    [TestClass]
    public class StarGenerationTests
    {
        [TestMethod]
        public void CreateBrandNewStarTest()
        {
            var generator = FactoryGenerator.RetrieveStarBuilder();
            Assert.IsInstanceOfType(generator.CreateBrandNewStar(), typeof(Star));
        }
    }
}
