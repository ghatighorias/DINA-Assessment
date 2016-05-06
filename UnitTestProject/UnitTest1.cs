using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class GalecticRomanAssignmentValueHandlerUnitTest
    {
        GalecticRomanAssignmentValueHandler grAssignment = new GalecticRomanAssignmentValueHandler();

        private void setUpInitialInput()
        {
            grAssignment.ParsedAssignment("glob", "I");
            grAssignment.ParsedAssignment("prok", "V");
            grAssignment.ParsedAssignment("pish", "X");
            grAssignment.ParsedAssignment("tegj", "L");
        }

        [TestMethod]
        public void TestMethod1()
        {
           setUpInitialInput();
           Assert.AreEqual(grAssignment.QueryAssignedValue("glob"), RomanNumerals.I);
        }
        [TestMethod]
        public void TestMethod2()
        {
            setUpInitialInput();
            Assert.AreEqual(grAssignment.QueryAssignedValue("pish"), RomanNumerals.X);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(grAssignment.QueryAssignedValue("glob"), RomanNumerals.INVALID);
        }
        [TestMethod]
        public void TestMethod4()
        {
            setUpInitialInput();
            Assert.AreEqual(grAssignment.QueryAssignedValue("X"), RomanNumerals.INVALID);
        }
        [TestMethod]
        public void TestMethod5()
        {
            setUpInitialInput();
            Assert.AreEqual(grAssignment.QueryAssignedValue(""), RomanNumerals.INVALID);
        }
        [TestMethod]
        public void TestMethod6()
        {
            setUpInitialInput();
            Assert.AreEqual(grAssignment.QueryAssignedValue("pish,pish"), RomanNumerals.INVALID);
        }

    }
}
