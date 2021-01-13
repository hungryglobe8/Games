using System;
using SudokuSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class SudokuGridTester
    {
        /// <summary>
        /// Create a 3x3 grid, with focus set to center tile.
        /// </summary>
        public SudokuGrid SmallGrid()
        {
            SudokuGrid grid = new SudokuGrid(3);
            grid.activeCell = grid.cells[1, 1];
            return grid;
        }

        [TestMethod]
        public void SudokuGridNotNull()
        {
            Assert.IsNotNull(new SudokuGrid(1));
        }

        [TestMethod]
        public void SudokuGridHasSudokuCells()
        {
            var sut = SmallGrid();

            foreach (var cell in sut.cells)
            {
                Assert.IsTrue(cell is SudokuCell);
            }
        }

        [TestMethod]
        public void FocusStartsTopLeft()
        {
            var sut = new SudokuGrid(3);

            Assert.IsTrue(sut.activeCell.Equals(0, 0));
        }

        #region ShiftTests
        [TestMethod]
        public void ShiftRight()
        {
            var sut = SmallGrid();

            sut.ShiftRight();

            Assert.IsTrue(sut.activeCell.Equals(2, 1));
        }

        [TestMethod]
        public void ShiftRightWrapsAround()
        {
            var sut = SmallGrid();

            sut.activeCell = sut.cells[2, 1];
            sut.ShiftRight();

            Assert.IsTrue(sut.activeCell.Equals(0, 1));
        }

        [TestMethod]
        public void ShiftLeft()
        {
            var sut = SmallGrid();

            sut.ShiftLeft();

            Assert.IsTrue(sut.activeCell.Equals(0, 1));
        }

        [TestMethod]
        public void ShiftLeftWrapsAround()
        {
            var sut = SmallGrid();

            sut.activeCell = sut.cells[0, 1];
            sut.ShiftLeft();

            Assert.IsTrue(sut.activeCell.Equals(2, 1));
        }

        [TestMethod]
        public void ShiftUp()
        {
            var sut = SmallGrid();

            sut.ShiftUp();

            Assert.IsTrue(sut.activeCell.Equals(1, 0));
        }

        [TestMethod]
        public void ShiftUpWrapsAround()
        {
            var sut = SmallGrid();

            sut.activeCell = sut.cells[1, 0];
            sut.ShiftUp();

            Assert.IsTrue(sut.activeCell.Equals(1, 2));
        }

        [TestMethod]
        public void ShiftDown()
        {
            var sut = SmallGrid();

            sut.ShiftDown();

            Assert.IsTrue(sut.activeCell.Equals(1, 2));
        }

        [TestMethod]
        public void ShiftDownWrapsAround()
        {
            var sut = SmallGrid();

            sut.activeCell = sut.cells[1, 2];
            sut.ShiftDown();

            Assert.IsTrue(sut.activeCell.Equals(1, 0));
        }
        #endregion
    }
}
