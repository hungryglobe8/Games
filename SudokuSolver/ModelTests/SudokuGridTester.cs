using System;
using SudokuSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class SudokuGridTester
    {
        public SudokuGrid SmallGrid() => new SudokuGrid(3);

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
            var sut = SmallGrid();

            Assert.IsTrue(sut.activeCell.Equals(0, 0));
        }

        [TestMethod]
        public void ShiftRight()
        {
            var sut = SmallGrid();

            sut.ShiftRight();

            Assert.IsTrue(sut.activeCell.Equals(1, 0));
        }
    }
}
