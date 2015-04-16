using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Generation.StarSystem;
using Moq;
using Models.Universe;
using BLL.Utilities.Structs;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    /// Summary description for OrbitGeneratorTest
    /// </summary>
    [TestClass]
    public class OrbitGeneratorTest
    {
        public OrbitGeneratorTest()
        {
            _Repo = new MockRepository(MockBehavior.Default);
            star = _Repo.Create<Star>();
            star.Object.Mass = 0.03;
            star.Object.Radius = 10;
        }

        private MockRepository _Repo;
        private Mock<Star> star;

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

        [TestMethod]
        public void TestGenerateType()
        {
            OrbitGenerator generator = new OrbitGenerator(star.Object, 0.4, 0.8, new DoubleRange(0.1, 0.6));
            Assert.IsInstanceOfType(generator.Generate(), typeof(OrbitDetail));
        }

        [TestMethod]
        public void TestCalculateDistanceTest()
        {
            OrbitGenerator generator = new OrbitGenerator(star.Object, 0.4, 0.8, new DoubleRange(0.1, 0.6));
            double distance = generator.CalculateDistanceTest();
            Assert.IsTrue(distance >= 0.7);
            generator = new OrbitGenerator(star.Object, 0.04, 0.6, new DoubleRange(0.1, 0.6));
            distance = generator.CalculateDistanceTest();
            Assert.IsTrue(distance >= 0.1 && distance<=0.6);
        }

        [TestMethod]
        public void TestCalculatePeriodOfRevolutionTest()
        {
            OrbitGenerator generator = new OrbitGenerator(star.Object, 0.4, 0.8, new DoubleRange(0.1, 0.6));
            double revolution = generator.CalculatePeriodOfRevolutionTest(5.2);
        }
    }
}
