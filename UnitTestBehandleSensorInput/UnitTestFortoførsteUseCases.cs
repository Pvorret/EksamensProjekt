using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EksamensProjekt.Controller;
using EksamensProjekt.Helper;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using System.Collections.Generic;

namespace UnitTest {
    [TestClass]
    public class UnitTestFortoførsteUseCases {
        [TestMethod]
        public void TestAddandGetSensorRuleFromSerialNumber() {
            RuleSetController rulesetcontroller = new RuleSetController();
            SensorController sensorcontroller = new SensorController();
            CitizenController citizencontroller = new CitizenController();
            //Random random = new Random();

            //Test data i databasen.
            int getSerialNumber = 888888;
            int sensorDependency1 = 999999;
            bool waitOrLook = true;
            int timeToWait = 2;
            int timeToLook = 0;
            bool whenToSend = true;
            string sensorType = "Test";

            //Test data til at ligge i databasen.
            int serialNumber = 111111;
            int sensorDependency2 = 111111;
            bool waitOrLook2 = true;
            int timeToWait2 = 2;
            int timeToLook2 = 1;
            bool whenToSend2 = false;
            string sensorType2 = "Dør";
            int sensorrulemanagementid2 = 0;

            //Når man adder sensorrulemanagement til databasen, retunere selve add metoden sensorrulemanagementid, som så skal bruges i AddSensorRuleFromSerialNumber.

            foreach (SensorRule sr in rulesetcontroller.GetSensorRuleFromSerialNumber(getSerialNumber)) {
                Assert.AreEqual(sr.SensorDependency, sensorDependency1);
                Assert.AreEqual(sr.WaitOrLook, waitOrLook);
                Assert.AreEqual(sr.TimeToWait, timeToWait);
                Assert.AreEqual(sr.TimeToLook, timeToLook);
                Assert.AreEqual(sr.WhenToSend, whenToSend);
            }

            Assert.AreEqual(getSerialNumber, SensorDBFacade.GetSensor(getSerialNumber)[0].SerialNumber);
            Assert.AreEqual(sensorType, SensorDBFacade.GetSensor(getSerialNumber)[0].Type);

            sensorcontroller.CreateSensor(serialNumber, sensorType2);

            foreach (Sensor s in SensorDBFacade.GetSensor(0)) {
                if (s.SerialNumber == serialNumber && s.Type == sensorType2) {
                    Assert.AreEqual(s.SerialNumber, serialNumber);
                    Assert.AreEqual(s.Type, sensorType);
                }
            }

            //sensorcontroller.ConnectSensorToCitizen();
            //citizencontroller.GetSensorsFromCitizen();

            //sensorrulemanagementid2 = sensrulesetcontroller.AddSensorRuleManagement("SR", serialNumber);
            //Skal også lige gøres for en timerangerule. Men det hele skal deles op i forhold til SR og TRR.


            rulesetcontroller.AddSensorRuleFromSerialNumber(serialNumber, sensorDependency2, waitOrLook2, timeToWait2, timeToLook2, whenToSend2, sensorrulemanagementid2);

            foreach (SensorRule sr in rulesetcontroller.GetSensorRuleFromSerialNumber(serialNumber)) {
                Assert.AreEqual(sr.SensorDependency, sensorDependency2);
                Assert.AreEqual(sr.WaitOrLook, waitOrLook2);
                Assert.AreEqual(sr.TimeToWait, timeToWait2);
                Assert.AreEqual(sr.TimeToLook, timeToLook2);
                Assert.AreEqual(sr.WhenToSend, whenToSend2);
            }

            sensorcontroller.DeleteSensor(serialNumber);

        }

        [TestMethod]
        public void TestAddAndGetTimeRangeRuleFromSensorSerialNumber() {
            //RuleSetController rulesetcontroller = new RuleSetController();
            //SensorController sensorcontroller = new SensorController();

            ////int randomSerialNumber = random.Next(0, 1000);
            //sensorcontroller.CreateSensor(randomSerialNumber, "Hej med dig Sensor");
            //string day = "Mandag";
            //DateTime startTime = new DateTime();
            //startTime.AddHours(10);
            //startTime.AddMinutes(00);
            //DateTime endTime = new DateTime();
            //endTime.AddHours(12);
            //endTime.AddMinutes(00);
            //string relativeCprNr = "123456-1234";
            //bool contactHelper = true;
            ////string actingRule = "SR " + rulesetcontroller.GetSensorRuleFromSerialNumber(randomSerialNumber)[0].Id.ToString();
            //foreach (SensorRule s in RuleSetDBFacade.GetSensorRuleFromSerialNumber(randomSerialNumber)) {
            //    string actingRule = "SR " + s.Id;
            //    rulesetcontroller.AddTimeRangeRuleFromSerialNumber(randomSerialNumber, day, startTime, endTime, relativeCprNr, actingRule, contactHelper);
            //    TimeRangeRule timerangerule = new TimeRangeRule(relativeCprNr, actingRule, contactHelper, new Time(startTime, endTime, day));
            //    foreach (TimeRangeRule t in rulesetcontroller.GetTimeRangeRuleFromSerialNumber(randomSerialNumber)) {
            //        Assert.AreEqual(t, timerangerule);
            //    }
            //}

            //sensorcontroller.DeleteSensor(randomSerialNumber);
        }
    }
}
