using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
namespace BLL.Generation.StarSystem.Tests
{
    [TestClass()]
    public class StarGenerationTests
    {
        [TestMethod()]
        public void CreateBrandNewStarTest()
        {
            StarGenerator generator = new StarGenerator();
            Assert.IsInstanceOfType(generator.CreateBrandNewStar(), typeof(Star));
        }
    }
}
