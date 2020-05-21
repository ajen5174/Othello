
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
}
