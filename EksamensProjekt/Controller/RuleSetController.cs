using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using SensorRuleSet; //HER ER DEN

namespace EksamensProjekt.Controller
{
    class RuleSetController
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

        public void GetSensorRuleFromSerialNumber(int serialnumber) {
            List<SensorRule> sensorruleList = new List<SensorRule>();

            foreach(SensorRule s in sensorruleList) {
                SensorRule sensorrule = new SensorRule(s.SensorDependency, s.WaitOrLook, s.TimeToWait, s.TimeToWait);
                SensorRuleSet.SensorRule.BehandleinputfraRuleSetController(sensorrule);
            }

            RuleSetDBFacade.GetSensorRuleFromSerialNumber(serialnumber);

            
        }
    }
}
