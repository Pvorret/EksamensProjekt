using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using EksamensProjekt.Model;
using SensorRuleSet;

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
    }
}
