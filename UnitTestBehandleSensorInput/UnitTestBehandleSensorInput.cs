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
            int SerialNumber = 220;
            bool wait = true;
            int timeToWait = 20;
            int timeToLook = 0;

            sensorcontroller.CreateSensor(SerialNumber, "Hej Sensor");

            List<int> intlist = new List<int>();

            foreach (Sensor s in SensorDBFacade.GetSensor(0)) {
                intlist.Add(s.SerialNumber);
            }
            int randomsensorDependency = intlist.ToArray()[random.Next(0, intlist.ToArray().Length)];

            SensorRule sensorrule = new SensorRule(randomsensorDependency, wait, timeToWait, timeToLook);


            rulesetcontroller.AddSensorRuleFromSerialNumber(SerialNumber, randomsensorDependency, wait, timeToWait, timeToLook);

            foreach (SensorRule s in rulesetcontroller.GetSensorRuleFromSerialNumber(SerialNumber)) {
                Assert.AreEqual(s, sensorrule);
            }

            sensorcontroller.DeleteSensor(SerialNumber);
           
        }

        [TestMethod]
        public void TestAddAndGetTimeRangeRuleFromSensorSerialNumber()
        {
            RuleSetController rulesetcontroller = new RuleSetController();
            SensorController sensorcontroller = new SensorController();
            Random random = new Random();
            int randomSerialNumber = random.Next(0, 1000);
            sensorcontroller.CreateSensor(randomSerialNumber, "Hej med dig Sensor");
            string day = "Mandag";
            DateTime startTime = new DateTime();
            startTime.AddHours(10);
            startTime.AddMinutes(00);
            DateTime endTime = new DateTime();
            endTime.AddHours(12);
            endTime.AddMinutes(00);
            string relativeCprNr = "123456-1234";
            bool contactHelper = true;
            //string actingRule = "SR " + rulesetcontroller.GetSensorRuleFromSerialNumber(randomSerialNumber)[0].Id.ToString();
            foreach (SensorRule s in RuleSetDBFacade.GetSensorRuleFromSerialNumber(randomSerialNumber)) {
                string actingRule = "SR " + s.Id;
                rulesetcontroller.AddTimeRangeRuleFromSerialNumber(randomSerialNumber, day, startTime, endTime, relativeCprNr, actingRule, contactHelper);
                TimeRangeRule timerangerule = new TimeRangeRule(relativeCprNr, actingRule, contactHelper, new Time(startTime, endTime, day));
                foreach (TimeRangeRule t in rulesetcontroller.GetTimeRangeRuleFromSerialNumber(randomSerialNumber)) {
                    Assert.AreEqual(t, timerangerule);
                }
            }

            sensorcontroller.DeleteSensor(randomSerialNumber);
        }
    }
}
