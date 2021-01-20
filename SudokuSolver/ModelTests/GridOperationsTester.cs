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
        public void AutoFill()
        {
            cells.AutoFill();
            bool result = cells.Any(cell => cell.Value == 0);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LockAllDoesNotWorkOnEmptyCells()
        {
            cells.LockAll();
            bool result = cells.All(cell => cell.IsLocked);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LockAllFilledCells()
        {
            cells.AutoFill();

            cells.LockAll();
            bool result = cells.All(cell => cell.IsLocked);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ClearEmptyCells()
        {
            cells.ClearAll();
            bool result = cells.All(cell => cell.Value == 0 && !cell.IsLocked && cell.IsValid);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ClearFilledCells()
        {
            cells.AutoFill();

            cells.ClearAll();
            bool result = cells.All(cell => cell.Value == 0 && !cell.IsLocked && cell.IsValid);

            Assert.IsTrue(result);
        }
    }
}
