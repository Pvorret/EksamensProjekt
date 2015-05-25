using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EksamensProjekt.Controller;

namespace UnitTest {
    [TestClass]
    public class UnitTestBehandleSensorInput {
        [TestMethod]
        public void TestMethod1() 
        {
            RuleSetController RSC = new RuleSetController();
            DateTime ActivationTime = new DateTime(2015, 05, 15, 10, 12, 00, 00);
            RSC.HandelSensorInput(1, ActivationTime);
            Assert.AreEqual(RSC.ContactList[0], RSC.TimeRangeRules[0].RelativeCPRNR);
            Assert.AreEqual(true, RSC.Contact);
            Assert.AreEqual("Helper", RSC.ContactList[0]);
            Assert.AreEqual("SR 69", RSC.TimeRangeRules[0].ActingRule);// skal lige ændres til et rigtig værdi
        }
    }
}
