
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Othello;
using Othello.Models;

namespace OthelloTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Equals()
        {
            Assert.AreEqual(10, 10);
        }

        [TestMethod]
        public void NotEquals()
        {
            Assert.AreNotEqual(11, 10);
        }
    }

    [TestClass]
    public class BoardTest
    {
        [UITestMethod]
        public void ReturnGrid()
        {
            MainPage mp = new MainPage();
            Assert.IsTrue(mp.CreateBoard());
        }
        
        [UITestMethod]
        public void GetValidSpaces()
        {
            MainPage mp = new MainPage();

            //initialize the board
            mp.stonesBoard = new Board();
            mp.stonesBoard.Spaces[3, 3].IsActive = true;//black

            mp.stonesBoard.Spaces[4, 3].IsActive = true;//white
            mp.stonesBoard.Spaces[4, 3].Flip();

            mp.stonesBoard.Spaces[3, 4].IsActive = true;//black
            mp.stonesBoard.Spaces[3, 4].Flip();

            mp.stonesBoard.Spaces[4, 4].IsActive = true;//white

            Stone[] blackValid = mp.stonesBoard.ValidSpaces(true);
            Stone[] whiteValid = mp.stonesBoard.ValidSpaces(false);

            Assert.AreEqual(4, blackValid.Length);//four possible moves for black
            Assert.AreEqual(4, whiteValid.Length);//four for white

        }

        [UITestMethod]
        public void CheckValidMove()
        {
            MainPage mp = new MainPage();

            //initialize the board
            mp.stonesBoard = new Board();
            mp.stonesBoard.Spaces[3, 3].IsActive = true;//black

            mp.stonesBoard.Spaces[4, 3].IsActive = true;//white
            mp.stonesBoard.Spaces[4, 3].Flip();

            mp.stonesBoard.Spaces[3, 4].IsActive = true;//black
            mp.stonesBoard.Spaces[3, 4].Flip();

            mp.stonesBoard.Spaces[4, 4].IsActive = true;//white

            Assert.IsTrue(mp.stonesBoard.CheckStoneIsValid(mp.stonesBoard.Spaces[2, 3], true));

        }

        [UITestMethod]
        public void CheckInvalidMove()
        {
            MainPage mp = new MainPage();

            //initialize the board
            mp.stonesBoard = new Board();
            mp.stonesBoard.Spaces[3, 3].IsActive = true;//black

            mp.stonesBoard.Spaces[4, 3].IsActive = true;//white
            mp.stonesBoard.Spaces[4, 3].Flip();

            mp.stonesBoard.Spaces[3, 4].IsActive = true;//black
            mp.stonesBoard.Spaces[3, 4].Flip();

            mp.stonesBoard.Spaces[4, 4].IsActive = true;//white

            Assert.IsFalse(mp.stonesBoard.CheckStoneIsValid(mp.stonesBoard.Spaces[5, 3], true));

        }

    }
}
