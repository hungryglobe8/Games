using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTester
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void TileIsCreated()
        {
            Tile sut = new Tile(false);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void UnarmedTileReturnsUnarmed()
        {
            Tile sut = new Tile(false);
            Assert.IsFalse(sut.IsArmed);
        }
        
        [TestMethod]
        public void ArmedTileReturnsArmed()
        {
            Tile sut = new Tile(true);
            Assert.IsTrue(sut.IsArmed);
        }

        [TestMethod]
        public void GetStatusIsCorrect()
        {
            Tile armedTile = new Tile(true);
            int exp = -1;
            int act = armedTile.GetDanger();
            Assert.AreEqual(exp, act);

            Tile unarmedTile = new Tile(false);
            exp = 0;
            act = unarmedTile.GetDanger();
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void IncreaseDangerIncrementsCounter()
        {
            Tile sut = new Tile(false);
            sut.DangerUp();
            Assert.AreEqual(1, sut.GetDanger());

            sut.DangerUp();
            Assert.AreEqual(2, sut.GetDanger());
        }
    }
}
