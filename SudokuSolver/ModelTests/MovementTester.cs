using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;

namespace ModelTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MovementTester
    {
        private SudokuCell[,] cells;
        private Movement movement;

        [TestInitialize]
        public void Init()
        {
            cells = SudokuGrid.CreateCells(3);
            movement = new Movement(cells);
        }

        private SudokuCell GetMiddleCell() => cells[1, 1];

        [TestMethod]
        public void MoveUp()
        {
            var sut = GetMiddleCell();

            var cell = movement.Up(sut);

            Assert.IsTrue(cell.Equals(1, 0));
        }

        [TestMethod]
        public void MoveUpWrapsAround()
        {
            var sut = cells[1, 0];

            var cell = movement.Up(sut);

            Assert.IsTrue(cell.Equals(1, 2));
        }

        [TestMethod]
        public void MoveDown()
        {
            var sut = GetMiddleCell();

            var cell = movement.Down(sut);

            Assert.IsTrue(cell.Equals(1, 2));
        }

        [TestMethod]
        public void MoveDownWrapsAround()
        {
            var sut = cells[1, 2];

            var cell = movement.Down(sut);

            Assert.IsTrue(cell.Equals(1, 0));
        }

        [TestMethod]
        public void MoveLeft()
        {
            var sut = GetMiddleCell();

            var cell = movement.Left(sut);

            Assert.IsTrue(cell.Equals(0, 1));
        }

        [TestMethod]
        public void MoveLeftWrapsAround()
        {
            var sut = cells[0, 1];

            var cell = movement.Left(sut);

            Assert.IsTrue(cell.Equals(2, 1));
        }

        [TestMethod]
        public void MoveRight()
        {
            var sut = GetMiddleCell();

            var cell = movement.Right(sut);

            Assert.IsTrue(cell.Equals(2, 1));
        }

        [TestMethod]
        public void MoveRightWrapsAround()
        {
            var sut = cells[2, 1];

            var cell = movement.Right(sut);

            Assert.IsTrue(cell.Equals(0, 1));
        }

        [DataTestMethod]
        [DataRow(2, 1, 2)]
        [DataRow(2, 2, 0)]
        public void JumpForward(int x, int y, int newY)
        {
            var sut = cells[x, y];

            var cell = movement.JumpForward(sut);

            Assert.IsTrue(cell.Equals(0, newY));
        }

        [DataTestMethod]
        [DataRow(0, 1, 0)]
        [DataRow(0, 2, 1)]
        public void JumpBackward(int x, int y, int newY)
        {
            var sut = cells[x, y];

            var cell = movement.JumpBackward(sut);

            Assert.IsTrue(cell.Equals(2, newY));
        }

        [TestMethod]
        public void JumpForwardSkipsCellWithValue()
        {
            var middle = GetMiddleCell();
            middle.SetValue(2);
            var sut = cells[0, 1];

            var actual = movement.JumpForward(sut);

            Assert.IsTrue(actual.Equals(2, 1));
        }

        [TestMethod]
        public void JumpBackwardSkipsCellWithValue()
        {
            var middle = GetMiddleCell();
            middle.SetValue(2);
            var sut = cells[2, 1];

            var actual = movement.JumpBackward(sut);

            Assert.IsTrue(actual.Equals(0, 1));
        }
    }
}
