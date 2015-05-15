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
        public bool WhenToSend { get; set; }
        public SensorRule(int id, int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook) {
            this.Id = id;
            this.SensorDependency = sensorDependency;
            this.WaitOrLook = waitOrLook;
            this.TimeToWait = timeToWait;
            this.TimeToLook = timeToLook;
        }

        public SensorRule(int sensorDependency, bool waitOrLook, int timeToWait, int timeToLook, bool whenToSend) {
            this.SensorDependency = sensorDependency;
            this.WaitOrLook = waitOrLook;
            this.TimeToWait = timeToWait;
            this.TimeToLook = timeToLook;
            this.WhenToSend = whenToSend;
        }
        public SensorRule() { }

        public void SensorRuleActivated(RuleSetController RSC, SensorRule sensorrule, DateTime activationTime) //af Thomas
        {
            List<SensorLog> sensorLogList = new List<SensorLog>();
            if (sensorrule.WaitOrLook == true)
            {
                DateTime checkeTime = new DateTime();
                checkeTime = DateTime.Now;
                bool sensorFound = false;
                DateTime WaitTime = new DateTime();
                WaitTime = activationTime.AddMinutes(sensorrule.TimeToWait);
                TimeSpan remainingTime = WaitTime - checkeTime;
                TimeSpan lastCheck = new TimeSpan(0, 0, 0);
                DateTime beforeCheck = DateTime.Now;
                while (lastCheck <= remainingTime && sensorFound == false)
                {
                    sensorLogList = RSC.GetSensorLogFromDateTime(beforeCheck);
                    foreach (SensorLog SL in sensorLogList)
                    {
                        if (SL.SerialNumber == sensorrule.SensorDependency && checkeTime < WaitTime)
                        {
                            if (sensorrule.WhenToSend == false)
                            {
                                RSC.Contact = false;
                            }
                            sensorFound = true;
                        } 
                    }
                    if (sensorFound == false)
                    {
                        if (sensorrule.WhenToSend == true)
                        {
                            RSC.Contact = false;
                        }
                    }
                    DateTime afterCheck = DateTime.Now;
                    lastCheck = afterCheck - beforeCheck;
                }
            }
            if (sensorrule.WaitOrLook == false)
            {
                DateTime checkeTime = new DateTime();
                checkeTime.AddMinutes(-sensorrule.TimeToLook);
                sensorLogList = RSC.GetSensorLogFromDateTime(checkeTime);
                foreach (SensorLog SL in sensorLogList)
                {
                    if (sensorrule.WhenToSend == true)
                    {
                        if (SL.SerialNumber != sensorrule.SensorDependency)
                        {
                            RSC.Contact = false;
                        }
                    }
                    if (sensorrule.WhenToSend == false)
                    {
                        if (SL.SerialNumber == sensorrule.SensorDependency)
                        {
                            RSC.Contact = false;
                        }
                    }
                }
            }
        }
    }
}
