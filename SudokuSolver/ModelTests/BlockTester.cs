using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;

namespace ModelTests
{
    [TestClass]
    public class BlockTester
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
        public void GetRows()
        {
            var sut = SmallGrid();

            var result = Block.GetHorizontal(sut);

            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.All(cell => cell.Y == 1));
        }

        [TestMethod]
        public void GetColumns()
        {
            var sut = SmallGrid();

            var result = Block.GetVertical(sut);

            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.All(cell => cell.X == 1));
        }

        [TestMethod]
        public void DownToUpDiagonal()
        {
            var sut = SmallGrid();

            var result = Block.BottomLeftToTopRight(sut);

            Assert.IsTrue(result.All(cell => cell.X + cell.Y == 2));
        }

        [TestMethod]
        public void UpToDownDiagonal()
        {
            var sut = SmallGrid();
            var expected = sut.cells[2, 2];

            var result = Block.TopLeftToBottomRight(sut);

            Assert.IsTrue(result.Contains(expected));
        }
    }
}
