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
            RuleSetController rulesetcontroller = new RuleSetController();
            SensorController sensorcontroller = new SensorController();
            Random random = new Random();
            int randomSerialNumber = random.Next(0, 1000);
            bool wait = true;
            int timeToWait = 20;
            int timeToLook = 0;

            sensorcontroller.CreateSensor(randomSerialNumber, "Hej Sensor");

            List<int> intlist = new List<int>();

            foreach (Sensor s in SensorDBFacade.GetSensor(0)) {
                intlist.Add(s.SerialNumber);
            }
            int randomsensorDependency = intlist.ToArray()[random.Next(0, intlist.ToArray().Length)];

            SensorRule sensorrule = new SensorRule(randomsensorDependency, wait, timeToWait, timeToLook);


            rulesetcontroller.AddSensorRuleFromSerialNumber(randomSerialNumber, randomsensorDependency, wait, timeToWait, timeToLook);

            foreach (SensorRule s in rulesetcontroller.GetSensorRuleFromSerialNumber(randomSerialNumber)) {
                Assert.AreEqual(s, sensorrule);
            }

            sensorcontroller.DeleteSensor(randomSerialNumber);


            //int[] row = new int[] { 1, 333388, 555446, 787547, 797976, 879465 };
            //int randomSerialNumber = row[random.Next(0, row.Length)];
           
        }

        [TestMethod]
        public void TestAddAndGetTimeRangeRuleFromSensorSerialNumber()
        {

        }
    }
}
