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

        #region Get Neighbors
        [TestMethod]
        [DataRow(0, 0, 3)]
        [DataRow(2, 0, 5)]
        [DataRow(3, 0, 3)]
        public void GetNeighborsBottomRow(int x, int y, int expected)
        {
            Field sut = new Field(4, 4, 6);
            Tile testTile = sut.GetTile(x, y);
            int actual = sut.GetNeighbors(testTile).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, 5)]
        [DataRow(2, 2, 8)]
        [DataRow(1, 3, 5)]
        public void GetNeighborsMiddleRow(int x, int y, int expected)
        {
            Field sut = new Field(4, 4, 6);
            Tile testTile = sut.GetTile(x, y);
            int actual = sut.GetNeighbors(testTile).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 3, 3)]
        [DataRow(1, 3, 5)]
        [DataRow(3, 3, 3)]
        public void GetNeighborsTopRow(int x, int y, int expected)
        {
            Field sut = new Field(4, 4, 6);
            Tile testTile = sut.GetTile(x, y);
            int actual = sut.GetNeighbors(testTile).Count;
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Populate Field
        [TestMethod]
        public void PopulateSmallFieldWithMinesUsingSeed()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile(), 10);
            Tile res = sut.GetTile(2, 0);
            Assert.IsTrue(res.IsArmed);

            res = sut.GetTile(2, 2);
            Assert.IsTrue(res.IsArmed);
        }

        [TestMethod]
        public void PopulateMediumFieldWithMinesUsingSeed()
        {
            Field sut = new Field(4, 4, 6);
            sut.PopulateField(new Tile(), 10);
            Assert.AreEqual(6, sut.NumMines);
        }
        #endregion

        #region Flag
        [TestMethod]
        public void FlagTile()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile());
            Tile tile = sut.GetTile(1, 2);
            sut.Flag(tile);
            Assert.AreEqual(1, sut.NumFlags);
        }

        [TestMethod]
        public void FlagTileTwice()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile());
            Tile tile = sut.GetTile(1, 2);
            sut.Flag(tile);
            sut.Flag(tile);
            Assert.AreEqual(2, sut.NumFlags);
        }

        [TestMethod]
        public void FlagAll()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile());
            var tiles = sut.GetTiles();
            foreach (Tile tile in tiles)
            {
                sut.Flag(tile);
            }
            Assert.AreEqual(0, sut.NumFlags);
            Assert.AreEqual(2, sut.NumMines);
        }
        #endregion

        #region Reveal
        [TestMethod]
        public void RevealTile()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile(), 3);
            Tile tile = sut.GetTile(0, 0);
            sut.Reveal(tile);
            Assert.AreEqual(State.Revealed, tile.state);
        }        
        
        [TestMethod]
        public void RevealBombTile()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile(), 3);
            Tile tile = sut.GetTile(0, 2);
            sut.Reveal(tile);
            Assert.AreEqual(State.Revealed, tile.state);
        }        
        
        [TestMethod]
        public void RevealFlaggedTileNoChange()
        {
            Field sut = new Field(3, 3, 2);
            sut.PopulateField(new Tile(), 3);
            Tile tile = sut.GetTile(0, 2);
            sut.Flag(tile);
            sut.Reveal(tile);
            Assert.AreEqual(State.Flagged, tile.state);
        }

        [TestMethod]
        public void RevealAllNeighborsOfZeroDangerTiles()
        {
            Field sut = new Field(3, 3, 0);
            sut.PopulateField(new Tile(), 3);
            Tile tile = sut.GetTile(0, 0);
            var result = sut.Reveal(tile);
            Assert.AreEqual(9, result.Count);
        }
        #endregion
    }
}
