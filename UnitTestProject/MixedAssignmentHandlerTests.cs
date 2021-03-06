﻿using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MixedAssignmentHandlerTests
    {


        GalecticRomanAssignmentValueHandler grAssignment;
        MixedAssignmentHandler mixedHandler;


        [TestInitialize()]
        public void setUpInitialInput()
        {
            grAssignment = new GalecticRomanAssignmentValueHandler();
            mixedHandler = new MixedAssignmentHandler(grAssignment);

            grAssignment.ParsedAssignment("glob", "I");
            grAssignment.ParsedAssignment("prok", "V");
            grAssignment.ParsedAssignment("pish", "X");
            grAssignment.ParsedAssignment("tegj", "L");
            mixedHandler.ParsedAssignment("glob", "glob", "Silver", "34");
            mixedHandler.ParsedAssignment("glob", "prok", "Gold", "57800");
            mixedHandler.ParsedAssignment("pish", "pish", "Iron", "3910");
        }
        


        [TestMethod]
        public void TestMethod1()
        {
            float retrievedValue;
            retrievedValue = mixedHandler.QueryAssignedValue("Silver");
            Assert.AreEqual(retrievedValue, 17);
        }

        [TestMethod]
        public void TestMethod2()
        {
            float retrievedValue;
            retrievedValue = mixedHandler.QueryAssignedValue("Gold");
            Assert.AreEqual(retrievedValue, 14450);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(false, mixedHandler.Contains(""));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(false, mixedHandler.Contains("Bronze"));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(false, mixedHandler.Contains("silver"));
        }
    }
}
