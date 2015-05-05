using BLL.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest.Utilities
{
    [TestClass]
    public class TimeConverterTest
    {
        [TestMethod]
        public void TestTimeConverter()
        {
            Assert.AreEqual("245", TimeConverter.FromDoubleToTime(0.67));
        }
    }
}