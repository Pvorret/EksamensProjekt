using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model {
    class SensorRule {

        public int SensorDependency { get; set; }
        public bool WaitOrLook { get; set; }
        public int TimeToWait { get; set; }
        public int TimeToLook { get; set; }

        public SensorRule(int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook) {
            this.SensorDependency = sensorDependency;
            this.WaitOrLook = waitOrLook;
            this.TimeToWait = timeToWait;
            this.TimeToLook = timeToWait;
        }

        public static void BehandleinputfraRuleSetController(SensorRule sensorrule) {
            //Navnet skal ændres og der skal sættes en return værdi.
            //Der skal kodes hvad der skal behandles.
        }
    }
}
