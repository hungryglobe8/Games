using System;
using SudokuSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace ModelTests
{
    [TestClass]
    public class SudokuCellTester
    {
        [TestMethod]
        public void SudokuCellNotNull()
        {
            Assert.IsNotNull(new SudokuCell());
        }

        [TestMethod]
        public void SudokuCellIsButton()
        {
            Assert.IsTrue(new SudokuCell() is Button);
        }

        private SudokuCell TopLeftCell()
        {
            return new SudokuCell
            {
                X = 0,
                Y = 0
            };
        }

        [TestMethod]
        public void SetXandY()
        {
            var sut = TopLeftCell();

            Assert.AreEqual(0, sut.X);
            Assert.AreEqual(0, sut.Y);
        }

        [TestMethod]
        public void EqualToValues()
        {
            var sut = TopLeftCell();

            Assert.IsTrue(sut.Equals(0, 0));
        }

        [TestMethod]
        public void EqualToOtherSudokuCell()
        {
            var sut = TopLeftCell();
            var other = TopLeftCell();

            Assert.IsTrue(sut.Equals(other));
        }
    }
}
