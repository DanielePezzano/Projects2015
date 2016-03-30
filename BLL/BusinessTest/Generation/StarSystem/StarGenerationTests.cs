using BLL.Generation.StarSystem;
using BLL.Generation.StarSystem.Builders;
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
            var generator = new StarBuilder();
            Assert.IsInstanceOfType(generator.CreateBrandNewStar(), typeof(Star));
        }
    }
}
