
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Othello;

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
    }
}
