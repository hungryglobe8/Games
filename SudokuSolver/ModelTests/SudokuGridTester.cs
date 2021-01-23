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
            SudokuGrid grid = new SudokuGrid(3, 3, 3);
            grid.Select(1, 1);
            return grid;
        }

        [TestMethod]
        public void SudokuGridNotNull()
        {
            Assert.IsNotNull(new SudokuGrid(1, 1, 1));
        }

        [TestMethod]
        public void AllCellsHaveRightNeighbors()
        {
            var sut = SmallGrid();

            foreach (var cell in sut.cells)
            {
                var result = sut.GetNeighbors(cell);
                Assert.AreEqual(9, result.Count);
            }
        }

        [TestMethod]
        public void AllCellsHaveRightNeighborsBigGrid()
        {
            var sut = new SudokuGrid(3, 3, 9);

            foreach (var cell in sut.cells)
            {
                var result = sut.GetNeighbors(cell);
                Assert.AreEqual(21, result.Count);
            }
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
        public void ActiveCellStartsTopLeft()
        {
            var sut = new SudokuGrid(3, 3, 3);

            Assert.IsTrue(sut.activeCell.Equals(0, 0));
        }

        [TestMethod]
        public void SelectCellChangesActiveCell()
        {
            var sut = SmallGrid();

            sut.Select(2, 2);

            Assert.IsTrue(sut.activeCell.Equals(2, 2));
        }

        #region SizeTests
        [DataTestMethod]
        [DataRow (3, 3, 9, 81)]
        [DataRow (2, 2, 4, 16)]
        [DataRow (2, 3, 6, 36)]
        [DataRow]
        public void CreateGridDiffSizes(int width, int height, int size, int numCells)
        {
            var sut = new SudokuGrid(width, height, size);

            Assert.AreEqual(numCells, sut.cells.Length);
        }
        #endregion

        #region LockTests
        [TestMethod]
        public void SingleLock()
        {

        }
        #endregion

        [TestMethod]
        public void FillGrid()
        {
            var sut = SmallGrid();

            for (int i = 1; i < 10; i++)
            {
                sut.ModifyCell(i);
            }

            Assert.IsTrue(sut.AllCellsFilled);
        }
    }
}
