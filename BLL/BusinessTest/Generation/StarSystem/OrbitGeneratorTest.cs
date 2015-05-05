using System;
using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using Moq;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    ///     Summary description for OrbitGeneratorTest
    /// </summary>
    [TestClass]
    public class OrbitGeneratorTest
    {
        public static Random Rnd;
        private readonly Mock<Star> _star;

        public OrbitGeneratorTest()
        {
            var repo = new MockRepository(MockBehavior.Default);
            _star = repo.Create<Star>();
            _star.Object.Mass = 0.03;
            _star.Object.Radius = 10;
            if (Rnd == null) Rnd = new Random(Environment.TickCount);
        }

        [TestMethod]
        public void TestGenerateType()
        {
            var generator = new OrbitGenerator(_star.Object, 0.8, new DoubleRange(0.1, 0.6));
            Assert.IsInstanceOfType(generator.Generate(Rnd), typeof (OrbitDetail));
        }

        [TestMethod]
        public void TestCalculateDistanceTest()
        {
            var generator = new OrbitGenerator(_star.Object, 0.8, new DoubleRange(0.1, 0.6));
            var distance = generator.CalculateDistanceTest();
            Assert.IsTrue(distance >= 0.7);
        }

        [TestMethod]
        public void TestCalculatePeriodOfRevolutionTest()
        {
            var generator = new OrbitGenerator(_star.Object, 0.8, new DoubleRange(0.1, 0.6));
            var revolution = generator.CalculatePeriodOfRevolutionTest(5.2);
            Assert.IsTrue(revolution>0);
        }

        [TestMethod]
        public void TestCalculatePeriodOfRotation()
        {
            var generator = new OrbitGenerator(_star.Object, 0.22, new DoubleRange(0.1, 0.6));
            var rotation = generator.CalculatePeriodOfRotation(0.45, 0.2, 4.9, Rnd);
            Assert.IsTrue(Math.Abs(rotation) > 40 && Math.Abs(rotation) < 100);
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}