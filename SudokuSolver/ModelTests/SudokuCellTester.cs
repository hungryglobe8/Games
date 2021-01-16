using System;
using SudokuSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace ModelTests
{
    [TestClass]
    public class SudokuCellTester
    {
        [TestMethod]
        public void SudokuCellNotNull()
        {
            Assert.IsNotNull(new SudokuCell(0, 0));
        }

        [TestMethod]
        public void SudokuCellIsButton()
        {
            Assert.IsTrue(new SudokuCell(0, 0) is Button);
        }

        [TestMethod]
        public void SetXandY()
        {
            var sut = new SudokuCell(0, 0);

            Assert.AreEqual(0, sut.X);
            Assert.AreEqual(0, sut.Y);
        }

        private SudokuCell BasicCell()
        {
            return new SudokuCell(0, 0);
        }

        [TestMethod]
        public void EqualToValues()
        {
            var sut = BasicCell();

            Assert.IsTrue(sut.Equals(0, 0));
        }

        [TestMethod]
        public void EqualToOtherSudokuCell()
        {
            var sut = BasicCell();
            var other = BasicCell();

            Assert.IsTrue(sut.Equals(other));
        }

        [DataTestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(1, 1)]
        public void NotEqualToDiffCoor(int x, int y)
        {
            var sut = BasicCell();
            var other = new SudokuCell(x, y);

            Assert.IsFalse(sut.Equals(x, y));
            Assert.IsFalse(sut.Equals(other));
        }

        [TestMethod]
        public void NotEqualToNull()
        {
            var sut = BasicCell();

            Assert.IsFalse(sut.Equals(null));
        }

        [TestMethod]
        public void NotEqualToOtherType()
        {
            var sut = BasicCell();
            var other = new Button();

            Assert.IsFalse(sut.Equals(other));
        }

        [TestMethod]
        public void SetValue()
        {
            var sut = BasicCell();

            sut.SetValue(5);

            Assert.AreEqual(5, sut.Value);
        }

        [TestMethod]
        public void DoesNotSetValueIfLocked()
        {
            var sut = BasicCell();

            sut.SetValue(1);
            sut.Lock();
            sut.SetValue(5);

            Assert.AreNotEqual(5, sut.Value);
        }

        [TestMethod]
        public void ClearCell()
        {
            var sut = BasicCell();

            sut.SetValue(5);
            sut.SetValue(0);

            Assert.AreEqual(0, sut.Value);
        }

        [TestMethod]
        public void CellWillNotClearIfLocked()
        {
            var sut = BasicCell();

            sut.SetValue(5);
            sut.Lock();
            sut.SetValue(0);

            Assert.AreEqual(5, sut.Value);
            Assert.IsTrue(sut.IsLocked);
        }

        [TestMethod]
        public void CreateConflictAffectsBothCells()
        {
            var sut = BasicCell();
            var other = BasicCell();

            sut.AddConflict(other);

            Assert.IsFalse(sut.IsValid);
            Assert.IsFalse(other.IsValid);
        }

        [TestMethod]
        public void RemoveConflictsClearsCell()
        {
            var sut = BasicCell();
            var c1 = BasicCell();
            var c2 = BasicCell();

            sut.AddConflict(c1);
            sut.AddConflict(c2);
            Assert.AreEqual(2, sut.Conflicts.Count);

            sut.RemoveConflicts();
            Assert.IsTrue(sut.IsValid);
        }

        [TestMethod]
        public void DoesNotLockEmpty()
        {
            var sut = BasicCell();

            sut.Lock();

            Assert.IsFalse(sut.IsLocked);
        }
    }
}
