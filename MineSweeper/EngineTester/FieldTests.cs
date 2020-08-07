using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTester
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void Init()
        {
            Field sut = new Field(5, 5, 15);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Create4by4WithSixMines()
        {
            Field sut = new Field(4, 4, 6);
            int expectedMines = 6;
            int actualMines = sut.NumMines;
            Assert.AreEqual(expectedMines, actualMines);
        }

        [TestMethod]
        public void GetNeighborsTopLeftEqualsThree()
        {
            Field sut = new Field(4, 4, 6);
            int expectedNeighbors = 3;
            int actualNeighbors = sut.GetNeighbors(0, 0).Count;
            Assert.AreEqual(expectedNeighbors, actualNeighbors);
        }

        [TestMethod]
        public void PopulateField()
        {
            Field sut = new Field(4, 4, 6);
            sut.PopulateField();
            Assert.AreEqual(0, 0);
        }
    }
}
