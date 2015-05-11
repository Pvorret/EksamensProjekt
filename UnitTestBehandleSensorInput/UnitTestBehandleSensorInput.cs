using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EksamensProjekt.Controller;
using EksamensProjekt.Helper;
using EksamensProjekt.Controller.DBFacades;

namespace UnitTest {
    [TestClass]
    public class UnitTestBehandleSensorInput {
        [TestMethod]
        public void TestAddandGetSensorRuleFromSerialNumber() {
            //Random random = new Random();
            //int randomSerialNumber = random.Next(0, 1000);
            //int sensorDependency = 1777;
            //bool wait = true;
            //int timeToWait = 20;
            //int timeToLook = 0;

            //SensorRule sensorrule = new SensorRule(sensorDependency, wait, timeToWait, timeToLook);

            //RuleSetController rulesetcontroller = new RuleSetController();
            //rulesetcontroller.AddSensorRuleFromSerialNumber(randomSerialNumber, sensorDependency, wait, timeToWait, timeToLook);

            //foreach (SensorRule s in rulesetcontroller.GetSensorRuleFromSerialNumber(randomSerialNumber))
            //{
            //    Assert.AreEqual(s, sensorrule);
            //}
        }
        [TestMethod]
        public void TestAddAndGetTimeRangeRuleFromSensorSerialNumber()
        {

        }
    }
}
