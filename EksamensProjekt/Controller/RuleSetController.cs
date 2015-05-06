using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Controller.DBFacades;
using SensorRuleSets;

namespace EksamensProjekt.Controller
{
    class RuleSetController
    {
        public int SensorLogID { get; set; }

        public void CreateSensorLog(int SerialNumber, string activationTime)
        {
            SensorLogID = RuleSetDBFacade.CreateSensorLog(SerialNumber, activationTime);
        }

        public void UpdateSensorLog(string contactTime, string contactPerson, string contactMessage)
        {
            RuleSetDBFacade.UpdateSensorLog(SensorLogID, contactTime, contactPerson, contactMessage);
        }
    }
}
