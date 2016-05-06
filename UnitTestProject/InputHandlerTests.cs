using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    //Making regex functions for checking the sentences private and fixing the tests accordingly
    [TestClass]
    public class InputHandlerTests
    {
        InputHandler inputHandler;
        const string InvalidQuestion = "";
        string output;

        [TestInitialize]
        public void Initialize()
        {
            inputHandler = new InputHandler();
            
            inputHandler.ParseInput("glob is I", out output);
            inputHandler.ParseInput("prok is V", out output);
            inputHandler.ParseInput("pish is X", out output);
            inputHandler.ParseInput("tegj is L", out output);
            inputHandler.ParseInput("glob glob Silver is 34 Credits", out output);
            inputHandler.ParseInput("glob prok Gold is 57800 Credits", out output);
            inputHandler.ParseInput("pish pish Iron is 3910 Credits", out output);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can not understand the sentence")]
        public void TestMethod1()
        {
            inputHandler.ParseInput("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?",out output);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can not understand the sentence")]
        public void TestMethod2()
        {
            inputHandler.ParseInput("", out output);
        }
        [TestMethod]
        public void TestMethod3()
        {
            inputHandler.ParseInput("how many Credits is glob prok Silver ?", out output);
            Assert.AreEqual(output, "glob prok Silver is 68");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An item with the same key has already been added.")]
        public void TestMethod4()
        {
            inputHandler.ParseInput("glob is I", out output);
            Assert.AreEqual(output,string.Empty);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An item with the same key has already been added.")]
        public void TestMethod5()
        {
            inputHandler.ParseInput("glob glob Silver is 34 Credits", out output);
            Assert.AreEqual(output, string.Empty);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod6()
        {
            inputHandler.ParseInput("how much is pish tegj glob glob", out output);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod7()
        {
            inputHandler.ParseInput("how much is test test glob glob", out output);
        }
    }
}
