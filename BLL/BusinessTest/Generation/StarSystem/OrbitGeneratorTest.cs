using System;
using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using Moq;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    ///     Summary description for OrbitGeneratorTest
    /// </summary>
    [TestClass]
    public class OrbitGeneratorTest
    {
        public static Random Rnd;
        private readonly Mock<StarDto> _star;

        public OrbitGeneratorTest()
        {
            var repo = new MockRepository(MockBehavior.Default);
            _star = repo.Create<StarDto>();
            _star.Object.Mass = 0.03;
            _star.Object.Radius = 10;
            if (Rnd == null) Rnd = new Random(Environment.TickCount);
        }

        [TestMethod]
        public void TestGenerateType()
        {
            var generator = new OrbitGenerator(_star.Object, new DoubleRange(0.1, 0.6),new SystemGenerationDto(){FoodPoor = false,FoodRich = false,ForceWater = false,ForceLiving = false,MostlyWater = false,MineralRich = false,MineralPoor = false});
            generator.AssignPlanetRadius(0.8);
            Assert.IsInstanceOfType(generator.Generate(Rnd), typeof (OrbitDetail));
        }
       

        [TestMethod]
        public void TestCalculatePeriodOfRotation()
        {
            var generator = new OrbitGenerator(_star.Object, new DoubleRange(0.1, 0.6),new SystemGenerationDto(){FoodPoor = false,FoodRich = false,ForceWater = false,ForceLiving = false,MostlyWater = false,MineralRich = false,MineralPoor = false});
            generator.AssignPlanetRadius(0.22);
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