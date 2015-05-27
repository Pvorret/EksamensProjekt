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
        RuleSetController rulesetcontroller = new RuleSetController();
        SensorController sensorcontroller = new SensorController();
        CitizenController citizencontroller = new CitizenController();

        [TestMethod]
        public void TestGetAllCitizen() {
            string Name = "Test Testensen";
            string CPRNR = "111111-2222";

            citizencontroller.GetAllCitizen();
            foreach (Citizen c in citizencontroller.Citizens) {
                Assert.AreEqual(c.Name, Name);
                Assert.AreEqual(c.CprNr, CPRNR);
            }
        }

        [TestMethod]
        public void TestCreateCitizen() {

        }

        [TestMethod]
        public void TestGetRelativeFromCitizen() {
            string CitizenCPRNR = "111111-2222";
            string RelativeName = "Testensen";
            string RelativeCPRNR = "222222-1111";
            //Inde i CitizenDatabseFacaden i GetRelativeFromCitizen, hvorfor er der et forloop som løber en liste igennem og så add til listen hvis der er noget i, og bagefter hvis der så ikke er noget i så lægger den også i.

            citizencontroller.GetAllCitizen();
            citizencontroller.GetRelativeFromCitizen(CitizenCPRNR);
            foreach (Citizen c in citizencontroller.Citizens) {
                foreach (Relative r in c.Relatives) {
                    Assert.AreEqual(r.Name, RelativeName);
                    Assert.AreEqual(r.CprNr, RelativeCPRNR);
                }
            }
        }

        [TestMethod]
        public void AddRelative() {

        }

        [TestMethod]
        public void TestGetSensor() {
            int getSerialNumber = 888888;
            string sensorType = "Test";

            Assert.AreEqual(getSerialNumber, SensorDBFacade.GetSensor(getSerialNumber)[0].SerialNumber);
            Assert.AreEqual(sensorType, SensorDBFacade.GetSensor(getSerialNumber)[0].Type);
        }

        [TestMethod]
        public void TestCreateSensor() {
            int serialNumber = 111111;
            string sensorType2 = "Dør";

            sensorcontroller.CreateSensor(serialNumber, sensorType2);

            foreach (Sensor s in SensorDBFacade.GetSensor(serialNumber)) {
                Assert.AreEqual(s.SerialNumber, serialNumber);
                Assert.AreEqual(s.Type, sensorType2);
            }   
        }

        [TestMethod]
        public void TestConnectSensorToCitizen() {
            int serialNumber = 111111;
            string CitizenCPRNR = "111111-2222";
            string SensorLocation = "Hoveddør";
            citizencontroller.GetAllCitizen();
            sensorcontroller.ConnectSensorToCitizen(serialNumber, CitizenCPRNR, SensorLocation);
            citizencontroller.GetSensorsFromCitizen(CitizenCPRNR);
            foreach (Citizen c in citizencontroller.Citizens) {
                if (c.CprNr == CitizenCPRNR) {
                    foreach (Sensor s in c.Sensors) {
                        Assert.AreEqual(s.SerialNumber, serialNumber);
                        Assert.AreEqual(s.Location, SensorLocation);
                    }
                }
                
            }
        }

        [TestMethod]
        public void TestGetSensorRuleFromSerialNumber() {
            int getSerialNumber = 888888;
            int sensorDependency1 = 999999;
            bool waitOrLook = true;
            int timeToWait = 2;
            int timeToLook = 0;
            bool whenToSend = true;

            foreach (SensorRule sr in rulesetcontroller.GetSensorRuleFromSerialNumber(getSerialNumber)) {
                Assert.AreEqual(sr.SensorDependency, sensorDependency1);
                Assert.AreEqual(sr.WaitOrLook, waitOrLook);
                Assert.AreEqual(sr.TimeToWait, timeToWait);
                Assert.AreEqual(sr.TimeToLook, timeToLook);
                Assert.AreEqual(sr.WhenToSend, whenToSend);
            }
        }

        [TestMethod]
        public void TestAddSensorRuleFromSerialNumber() {
            int serialNumber = 111111;
            int sensorDependency2 = 888888;
            bool waitOrLook2 = true;
            int timeToWait2 = 1;
            int timeToLook2 = 0;
            bool whenToSend2 = false;
            string RuleSet = "SR";
            int sensorrulemanagementid2 = 0;
            
            rulesetcontroller.AddSensorRuleManagement(RuleSet, serialNumber);

            sensorrulemanagementid2 = rulesetcontroller.SensorRuleManagementId;

            //Når man adder sensorrulemanagement til databasen, retunere selve add metoden sensorrulemanagementid, som så skal bruges i AddSensorRuleFromSerialNumber.
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
        }

        [TestMethod]
        public void TestAddTimeRangeRuleFormSerialNumber() {
            int serialNumber = 111111;
            string Day = "Monday";
            DateTime StartTime = Convert.ToDateTime("08:00");
            DateTime EndTime = Convert.ToDateTime("10:00");
            string RelativeCPRNR = "222222-1111"; //Ved heller ikke rigtig hvad jeg skal gøre med Relative, når jeg ikke rigtig kan teste på Get og Add - Relative.
            int SensorRuleId = 0;
            string ActingRule = "SR" + SensorRuleId;
            bool ContactHelper = true;
            int SensorSerialNumber = 443333; //Skal lige have fundet ud af hvad jeg gør her, om det bare skal være en som også hele tiden ligger ude i Databasen?
            int sensorDependency2 = 888888;
            bool waitOrLook2 = false;
            int timeToWait2 = 0;
            int timeToLook2 = 1;
            bool whenToSend2 = true;
            int sensorrulemanagementid2 = 0;
            string RuleSet = "TRR";

            //Jeg forstår ikke inde i vinduet AddSensorRuleOrTimeRangeRule, at hvis man har valgt sensor regel = TRR og man så her efter skal koble en sensor regle til.
            //at man så i koden opretter en SensorRule? Linje 182 og så derefter opretter en TimeRangeRule, for så det jo kun den regel man har at vælge i mellem.

            rulesetcontroller.AddSensorRuleManagement(RuleSet, serialNumber);
            sensorrulemanagementid2 = rulesetcontroller.SensorRuleManagementId;
            rulesetcontroller.AddSensorRuleFromSerialNumber(SensorSerialNumber, sensorDependency2, waitOrLook2, timeToWait2, timeToLook2, whenToSend2, sensorrulemanagementid2);
            SensorRuleId = rulesetcontroller.SensorRuleId;
            rulesetcontroller.AddTimeRangeRuleFromSerialNumber(serialNumber, Day, StartTime, EndTime, RelativeCPRNR, ActingRule, ContactHelper);
        }

        [TestMethod]
        public void DeleteSensor() {
            int serialNumber = 111111;
            sensorcontroller.DeleteSensor(serialNumber);

            Assert.AreEqual(serialNumber, SensorDBFacade.GetSensor(serialNumber)[0].SerialNumber);
        }
    }
}
