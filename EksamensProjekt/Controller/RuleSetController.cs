using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using EksamensProjekt.Helper;
using System.Text.RegularExpressions;
using System.Windows;

namespace EksamensProjekt.Controller
{
    public class RuleSetController
    {
        public SensorLog SensorLog;
        public List<SensorRule> SensorRules = new List<SensorRule>();
        public List<TimeRangeRule> TimeRangeRules = new List<TimeRangeRule>();
        public List<Citizen> CitizenList = new List<Citizen>();
        public List<string> ContactList = new List<string>();
        public bool Contact = true;
        public int SensorRuleId { get; set; }

        public void HandelSensorInput(int serialNumber, DateTime activationTime)
        {

        }
        public void GetSensorInputInformation(int serialNumber, DateTime activationTime) 
        { 
            SensorLog SL = new SensorLog(serialNumber, activationTime);
            RuleSetDBFacade.CreateSensorLog(SL);

        }
        public void CreateSensorLog(int serialNumber, string activationTime)
        {
            SensorLog = new SensorLog(serialNumber, Convert.ToDateTime(activationTime));
            SensorLog = RuleSetDBFacade.CreateSensorLog(SensorLog);
        }
        public void UpdateSensorLog(string contactTime, string contactPerson, string contactMessage)
        {
            SensorLog = new SensorLog();
            SensorLog.ContactTime = Convert.ToDateTime(contactTime);
            SensorLog.ContactPerson = contactPerson;
            SensorLog.ContactMessage = contactMessage;
            RuleSetDBFacade.UpdateSensorLog(SensorLog);
        }
        public void AddSensorRuleManagement(string ruleSet, int serialNumber) {
            RuleSetDBFacade.AddSensorRuleManagement(ruleSet, serialNumber);
        }
        public List<SensorRule> GetSensorRuleFromSerialNumber(int serialNumber) {
            foreach (SensorRule s in RuleSetDBFacade.GetSensorRuleFromSerialNumber(serialNumber)) {
                SensorRule sensorrule = new SensorRule(s.Id, s.SensorDependency, s.WaitOrLook, s.TimeToWait, s.TimeToWait);
                SensorRule.BehandleinputfraRuleSetController(sensorrule);
            }

            return RuleSetDBFacade.GetSensorRuleFromSerialNumber(serialNumber);
        }
        public void AddSensorRuleFromSerialNumber(int serialNumber, int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook, bool whenToSend)//Stefan
        {
            SensorRule sensorRule = new SensorRule(sensorDependency, waitOrLook, timeToWait, timeToLook, whenToSend);
            SensorRuleId = RuleSetDBFacade.AddSensorRuleFromSerialNumber(serialNumber, sensorRule);
        }
        public void AddTimeRangeRuleFromSerialNumber(int serialNumber, string day, DateTime startTime, DateTime endTime, string relativeCprNr, string actingRule, bool contactHelper) {
            TimeRangeRule timerange = new TimeRangeRule(relativeCprNr, actingRule, contactHelper, new Time(startTime, endTime, day));
            RuleSetDBFacade.AddTimeRangeRuleFromSerialNumber(serialNumber, timerange);
        }
        public Dictionary<string, int> GetSensorRuleManagementFromSerialNumber(int serialNumber)
        {
            return DBFacades.RuleSetDBFacade.GetSensorRuleManagementFromSerialNumber(serialNumber);
        }
        public void GetTimeRangeRuleFromSerialNumber(int serialNumber)
        {
            TimeRangeRules = DBFacades.RuleSetDBFacade.GetTimeRangeRuleFromSerialNumber(serialNumber);
        }
        public bool CheckTime(Time timeRange, DateTime activationTime)//Stefan
        {
            if (timeRange.Day == activationTime.DayOfWeek.ToString())
            {
                if (timeRange.StartTime <= activationTime)
                {
                    if (timeRange.EndTime >= activationTime)
                    {
                        return true;
                    }
                }
            }
            return false;          
        }
        public void CheckActing(string actingRuleString)
        {
            string[] rule = Regex.Split(actingRuleString, @"\W+");
            int id = int.Parse(rule[1]);
            if (rule[0] == "SR")
            {
                SensorRules.Add(RuleSetDBFacade.GetSensorRuleFromID(id));
            }
        }
        public void SendMessage(List<string> contactPersons)//Stefan
        {
            foreach (string CP in contactPersons)
            {
                if (CP == "Helper")
                {
                    MessageBox.Show("Send Besked til Hjemmehjælper");
                }
                else
                {
                    MessageBox.Show("Besked send til: " + CP);
                }
            }
        }
        public List<SensorLog> GetSensorLogFromDateTime(DateTime checkTime)
        {
            return SensorDBFacade.GetSensorLogFromDateTime(checkTime);
        }
    }
}
