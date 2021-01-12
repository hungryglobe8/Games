using System;
using SudokuSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class SudokuGridTester
    {
        [TestMethod]
        public void SudokuGridNotNull()
        {
            Assert.IsNotNull(new SudokuGrid(1));
        }

        [TestMethod]
        public void Sudoku()
        {
            
        }
    }
}
