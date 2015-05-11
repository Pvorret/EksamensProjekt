using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;
using EksamensProjekt.Controller;

namespace EksamensProjekt.Helper {
    public class SensorRule {
        public int Id { get; set; }
        public int SensorDependency { get; set; }
        public bool WaitOrLook { get; set; }
        public int TimeToWait { get; set; }
        public int TimeToLook { get; set; }

        public SensorRule(int id, int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook) {
            this.Id = id;
            this.SensorDependency = sensorDependency;
            this.WaitOrLook = waitOrLook;
            this.TimeToWait = timeToWait;
            this.TimeToLook = timeToLook;
        }

        public SensorRule(int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook) {
            this.SensorDependency = sensorDependency;
            this.WaitOrLook = waitOrLook;
            this.TimeToWait = timeToWait;
            this.TimeToLook = timeToLook;
        }

        public static void BehandleinputfraRuleSetController(SensorRule sensorrule) {
            //Navnet skal ændres og der skal sættes en return værdi.
            //Der skal kodes hvad der skal behandles.
        }
    }
}
