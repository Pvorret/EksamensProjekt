using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using EksamensProjekt.Helper;

namespace EksamensProjekt.Controller
{
    public class RuleSetController
    {
        public SensorLog SensorLog;
        
        

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

        public void GetSensorRuleFromSerialNumber(int serialNumber) {
            foreach (SensorRule s in RuleSetDBFacade.GetSensorRuleFromSerialNumber(serialNumber)) {
                SensorRule sensorrule = new SensorRule(s.SensorDependency, s.WaitOrLook, s.TimeToWait, s.TimeToWait);
                SensorRule.BehandleinputfraRuleSetController(sensorrule);
            }
        }
        public void AddSensorRuleFromSerialNumber(int serialNumber, int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook)//Stefan
        {
            SensorRule sensorRule = new SensorRule(sensorDependency, waitOrLook, timeToWait, timeToLook);
            RuleSetDBFacade.AddSensorRuleFromSerialNumber(serialNumber, sensorRule);
        }

        public void AddTimeRangeRuleFromSerialNumber(int serialNumber, string day, DateTime startTime, DateTime endTime, string relativeCprNr, string actingRule, bool contactHelper) {
            TimeRangeRule timerange = new TimeRangeRule(relativeCprNr, actingRule, new Time(startTime, endTime, day));
            RuleSetDBFacade.AddTimeRangeRuleFromSensorSerialNumber(serialNumber, timerange);
        }
        public Dictionary<string, int> GetSensorRuleManagementSensorSerialNumber(int serialNumber)
        {
            return DBFacades.RuleSetDBFacade.GetSensorRuleManagementFromSensorSerialNumber(serialNumber);
        }
        public List<TimeRangeRule> GetTimeRangeRuleFromSensorSerialNumber(int serialNumber)
        {
            return DBFacades.RuleSetDBFacade.GetTimeRangeRuleFromSensorSerialNumber(serialNumber);
        }
    }
}
