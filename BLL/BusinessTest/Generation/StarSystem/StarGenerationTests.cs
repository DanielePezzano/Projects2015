using BLL.Generation.StarSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;

namespace BusinessTest.Generation.StarSystem
{
    [TestClass()]
    public class StarGenerationTests
    {
        [TestMethod()]
        public void CreateBrandNewStarTest()
        {
            var generator = new StarGenerator();
            Assert.IsInstanceOfType(generator.CreateBrandNewStar(), typeof(Star));
        }
    }
}
