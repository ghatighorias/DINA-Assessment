using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    
    [TestClass]
    public class InputHandlerTests
    {
        InputHandler inputHandler;

        [TestInitialize]
        public void Initialize()
        {
            inputHandler = new InputHandler();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(inputHandler.par("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"), false);
            Assert.AreEqual(inputHandler.IsMixedAssignment("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"), false);
            Assert.AreEqual(inputHandler.IsQuestion("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"), false);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(inputHandler.IsGalecticalAssignment(""), false);
            Assert.AreEqual(inputHandler.IsMixedAssignment(""), false);
            Assert.AreEqual(inputHandler.IsQuestion(""), false);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(inputHandler.IsGalecticalAssignment("how many Credits is glob prok Silver ?"), false);
            Assert.AreEqual(inputHandler.IsMixedAssignment("how many Credits is glob prok Silver ?"), false);
            Assert.AreEqual(inputHandler.IsQuestion("how many Credits is glob prok Silver ?"), true);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(inputHandler.IsGalecticalAssignment("glob is I"), true);
            Assert.AreEqual(inputHandler.IsMixedAssignment("glob is I"), false);
            Assert.AreEqual(inputHandler.IsQuestion("glob is I"), false);
        }
        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(inputHandler.IsGalecticalAssignment("glob glob Silver is 34 Credits"), false);
            Assert.AreEqual(inputHandler.IsMixedAssignment("glob glob Silver is 34 Credits"), true);
            Assert.AreEqual(inputHandler.IsQuestion("glob glob Silver is 34 Credits"), false);
        }
        [TestMethod]
        public void TestMethod6()
        {
            Assert.AreEqual(inputHandler.IsGalecticalAssignment("how much is pish tegj glob glob"), false);
            Assert.AreEqual(inputHandler.IsMixedAssignment("how much is pish tegj glob glob"), false);
            Assert.AreEqual(inputHandler.IsQuestion("how much is pish tegj glob glob"), false);
        }
    }
}
