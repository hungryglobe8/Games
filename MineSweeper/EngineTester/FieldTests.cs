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
        [DataRow(0, 0, 3)]
        [DataRow(2, 0, 5)]
        [DataRow(3, 0, 3)]
        public void GetNeighborsBottomRow(int x, int y, int expected)
        {
            Field sut = new Field(4, 4, 6);
            int actual = sut.GetNeighbors(x, y).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, 5)]
        [DataRow(2, 2, 8)]
        [DataRow(1, 3, 5)]
        public void GetNeighborsMiddleRow(int x, int y, int expected)
        {
            Field sut = new Field(4, 4, 6);
            int actual = sut.GetNeighbors(x, y).Count;
            Assert.AreEqual(expected, actual);
        }        
        
        [TestMethod]
        [DataRow(0, 3, 3)]
        [DataRow(1, 3, 5)]
        [DataRow(3, 3, 3)]
        public void GetNeighborsTopRow(int x, int y, int expected)
        {
            Field sut = new Field(4, 4, 6);
            int actual = sut.GetNeighbors(x, y).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PopulateFieldWithMines()
        {
            Field sut = new Field(4, 4, 6);
            sut.PopulateField();
            Assert.AreEqual(0, 0);
        }
    }
}
