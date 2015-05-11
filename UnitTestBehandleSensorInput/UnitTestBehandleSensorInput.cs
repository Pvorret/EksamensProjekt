using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EksamensProjekt.Controller;
using EksamensProjekt.Helper;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using System.Collections.Generic;

namespace UnitTest {
    [TestClass]
    public class UnitTestBehandleSensorInput {
        [TestMethod]
        public void TestAddandGetSensorRuleFromSerialNumber() {
            Random random = new Random();
            //int[] row = new int[] { 1, 333388, 555446, 787547, 797976, 879465 };
            //int randomSerialNumber = row[random.Next(0, row.Length)];
            List<int> intlist = new List<int>();
            bool wait = true;
            int timeToWait = 20;
            int timeToLook = 0;
            foreach (Sensor s in SensorDBFacade.GetSensor(0)) {
                intlist.Add(s.SerialNumber);
            }
            
            for (int i = 0; i < intlist.Count; i++) {
                int randomSerialNumber = intlist.ToArray()[random.Next(0, intlist.ToArray().Length)];
                if (i != randomSerialNumber) {
                    int randomsensorDependency = intlist.ToArray()[random.Next(0, intlist.ToArray().Length)];
                    SensorRule sensorrule = new SensorRule(randomsensorDependency, wait, timeToWait, timeToLook);

                    RuleSetController rulesetcontroller = new RuleSetController();
                    rulesetcontroller.AddSensorRuleFromSerialNumber(randomSerialNumber, randomsensorDependency, wait, timeToWait, timeToLook);

                    foreach (SensorRule s in rulesetcontroller.GetSensorRuleFromSerialNumber(randomSerialNumber)) {
                        Assert.AreEqual(s, sensorrule);
                    }
                }
			}
        }
        [TestMethod]
        public void TestAddAndGetTimeRangeRuleFromSensorSerialNumber()
        {

        }
    }
}
