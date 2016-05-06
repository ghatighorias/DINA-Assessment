using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class RomanNumeralParsetTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("I", "I"))
            {

                Assert.AreEqual(parser.CalculateRomanNumeralSet("I", "I"), 2);
            }
            else
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod2()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("I", "II"))
                Assert.Fail();                
        }
        [TestMethod]
        public void TestMethod3()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("I", "V"))
            {

                Assert.AreEqual(parser.CalculateRomanNumeralSet("I", "V"), 4);
            }
            else
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod4()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("V", "I"))
            {

                Assert.AreEqual(parser.CalculateRomanNumeralSet("V", "I"), 6);
            }
            else
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod5()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("M", "C", "M", "X", "L", "I", "V"))
                Assert.AreEqual(parser.CalculateRomanNumeralSet("M", "C", "M", "X", "L", "I", "V"), 1944);
            else
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod6()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("I", ""))
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod7()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("F", "I"))
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod8()
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            if (parser.TryParse("test", "I"))
                Assert.Fail();
        }

    }
}
