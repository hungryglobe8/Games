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
            grid.activeCell = grid.cells[1, 1];
            grid.activeCell.Select();
            return grid;
        }

        [TestMethod]
        public void SudokuGridNotNull()
        {
            Assert.IsNotNull(new SudokuGrid(1, 1, 1));
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

            sut.SelectCell(sut.cells[2, 2]);

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

        [DataTestMethod]
        [DataRow(2, 1, 2)]
        [DataRow(2, 2, 0)]
        public void JumpForward(int x, int y, int newY)
        {
            var sut = SmallGrid();
            sut.SelectCell(sut.cells[x, y]);
            sut.activeCell.SetValue(1);

            sut.JumpForward();

            Assert.IsTrue(sut.activeCell.Equals(0, newY));
        }

        [DataTestMethod]
        [DataRow(0, 1, 0)]
        [DataRow(0, 2, 1)]
        public void JumpBackward(int x, int y, int newY)
        {
            var sut = SmallGrid();
            sut.SelectCell(sut.cells[x, y]);
            sut.activeCell.SetValue(1);

            sut.JumpBackward();

            Assert.IsTrue(sut.activeCell.Equals(2, newY));
        }

        public void JumpSkipsCellWithValue()
        {
            var sut = SmallGrid();
            sut.ModifyCell(2);

            sut.JumpBackward();
            Assert.IsTrue(sut.activeCell.Equals(2, 2));

            sut.JumpForward();
            Assert.IsTrue(sut.activeCell.Equals(1, 0));
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

            Assert.IsTrue(sut.AllCellsFilled());
        }
    }
}
