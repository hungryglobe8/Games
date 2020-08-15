using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTester
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void Init()
        {
            Tile sut = new Tile();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void NewTileIsUnopened()
        {
            Tile sut = new Tile();
            var expectedState = State.Unopened;
            var actualState = sut.state;
            Assert.AreEqual(expectedState, actualState);
        }

        [TestMethod]
        public void LeftClickChangesStateToRevealed()
        {
            Tile sut = new Tile();
            var expectedState = State.Revealed;
            sut.LeftClick();
            var actualState = sut.state;
            Assert.AreEqual(expectedState, actualState);
        }
        
        [TestMethod]
        public void RightClickFlagsTile()
        {
            Tile sut = new Tile();
            var expectedState = State.Flagged;
            sut.RightClick();
            var actualState = sut.state;
            Assert.AreEqual(expectedState, actualState);
        }

        [TestMethod]
        public void RightClickDoesNotWorkOnRevealedTile()
        {
            Tile sut = new Tile();
            var expectedState = State.Revealed;
            sut.LeftClick();
            sut.RightClick();
            var actualState = sut.state;
            Assert.AreEqual(expectedState, actualState);
        }

        [TestMethod]
        public void LeftClickDoesNotWorkOnFlaggedTile()
        {
            Tile sut = new Tile();
            var expectedState = State.Flagged;
            sut.RightClick();
            sut.LeftClick();
            var actualState = sut.state;
            Assert.AreEqual(expectedState, actualState);
        }

        [TestMethod]
        public void UnarmedTileReturnsUnarmed()
        {
            Tile sut = new Tile();
            Assert.IsFalse(sut.IsArmed);
        }
        
        [TestMethod]
        public void ArmedTileReturnsArmed()
        {
            Tile sut = new Tile(armed: true);
            Assert.IsTrue(sut.IsArmed);
        }

        [TestMethod]
        public void GetStatusOfInitializedTilesIsCorrect()
        {
            Tile armedTile = new Tile(armed: true);
            int exp = 10;
            int act = armedTile.GetDanger();
            Assert.AreEqual(exp, act);

            Tile unarmedTile = new Tile();
            exp = 0;
            act = unarmedTile.GetDanger();
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void IncreaseDangerIncrementsCounter()
        {
            Tile sut = new Tile();
            sut.DangerUp();
            Assert.AreEqual(1, sut.GetDanger());

            sut.DangerUp();
            Assert.AreEqual(2, sut.GetDanger());
        }

        [TestMethod]
        public void IncreaseDangerDoesNotAffectBombCounter()
        {
            Tile sut = new Tile(armed: true);
            sut.DangerUp();
            Assert.AreEqual(10, sut.GetDanger());
        }
    }
}
