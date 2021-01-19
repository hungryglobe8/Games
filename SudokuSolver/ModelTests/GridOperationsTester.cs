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
    public class GridOperationsTester
    {
        [TestMethod]
        public void SudokuGridNotNull()
        {
            Assert.IsNotNull(GridOperations.Create(3));
        }

        private SudokuCell[,] cells = GridOperations.Create(3);

        [TestMethod]
        public void SudokuGridHasSudokuCells()
        {
            bool result = cells.All(cell => cell is SudokuCell);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LockAll()
        {
            cells.LockAll();
            bool result = cells.All(cell => cell.IsLocked);

            Assert.IsTrue(result);
        }
    }
}
