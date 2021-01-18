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
        SudokuCell middle = new SudokuCell(1, 1);

        public Movement GetMovement()
        {
            SudokuGrid grid = new SudokuGrid(3, 3, 3);
            Movement movement = new Movement(grid.cells);
            return movement;
        }

        [TestMethod]
        public void MoveUp()
        {
            var sut = GetMovement();

            var cell = sut.Up(middle);

            Assert.IsTrue(cell.Equals(1, 0));
        }

        [TestMethod]
        public void MoveUpWrapsAround()
        {
            var sut = GetMovement();

            var cell = sut.Up(sut.Up(middle));

            Assert.IsTrue(cell.Equals(1, 2));
        }

        [TestMethod]
        public void MoveDown()
        {
            var sut = GetMovement();

            var cell = sut.Down(middle);

            Assert.IsTrue(cell.Equals(1, 2));
        }

        [TestMethod]
        public void MoveDownWrapsAround()
        {
            var sut = GetMovement();

            var cell = sut.Down(sut.Down(middle));

            Assert.IsTrue(cell.Equals(1, 0));
        }

        [TestMethod]
        public void MoveLeft()
        {
            var sut = GetMovement();

            var cell = sut.Left(middle);

            Assert.IsTrue(cell.Equals(0, 1));
        }

        [TestMethod]
        public void MoveLeftWrapsAround()
        {
            var sut = GetMovement();

            var cell = sut.Left(sut.Left(middle));

            Assert.IsTrue(cell.Equals(2, 1));
        }
        [TestMethod]
        public void MoveRight()
        {
            var sut = GetMovement();

            var cell = sut.Right(middle);

            Assert.IsTrue(cell.Equals(2, 1));
        }

        [TestMethod]
        public void MoveRightWrapsAround()
        {
            var sut = GetMovement();

            var cell = sut.Right(sut.Right(middle));

            Assert.IsTrue(cell.Equals(0, 1));
        }


        //[DataTestMethod]
        //[DataRow(2, 1, 2)]
        //[DataRow(2, 2, 0)]
        //public void JumpForward(int x, int y, int newY)
        //{
        //    var sut = SmallGrid();
        //    sut.SelectCell(sut.cells[x, y]);
        //    sut.activeCell.SetValue(1);

        //    sut.JumpForward();

        //    Assert.IsTrue(sut.activeCell.Equals(0, newY));
        //}

        //[DataTestMethod]
        //[DataRow(0, 1, 0)]
        //[DataRow(0, 2, 1)]
        //public void JumpBackward(int x, int y, int newY)
        //{
        //    var sut = SmallGrid();
        //    sut.SelectCell(sut.cells[x, y]);
        //    sut.activeCell.SetValue(1);

        //    sut.JumpBackward();

        //    Assert.IsTrue(sut.activeCell.Equals(2, newY));
        //}

        //public void JumpSkipsCellWithValue()
        //{
        //    var sut = SmallGrid();
        //    sut.ModifyCell(2);

        //    sut.JumpBackward();
        //    Assert.IsTrue(sut.activeCell.Equals(2, 2));

        //    sut.JumpForward();
        //    Assert.IsTrue(sut.activeCell.Equals(1, 0));
        //}
    }
}
