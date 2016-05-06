using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment;
using System;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test = "item1 item2 item3";
            string[] splitedByUtility = Utility.SplitString(test, " ", StringSplitOptions.RemoveEmptyEntries);
            string[] splitedDirectly = test.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(splitedByUtility.SequenceEqual(splitedDirectly), true);
        }
    }
}