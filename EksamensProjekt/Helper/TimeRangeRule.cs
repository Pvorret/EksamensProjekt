using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.Model;
using EksamensProjekt.Controller;

namespace EksamensProjekt.Helper {
    class TimeRangeRule {
        public string CPRNR { get; set; }
        public string ActingRule { get; set; }
        public bool ContactHelper { get; set; }
        public Time Time { get; set; }

        public TimeRangeRule(string cpr, string actingRule, Time time) 
        {
            this.ActingRule = actingRule;
            this.CPRNR = cpr;
            this.Time = time;
        }
        public List<TimeRangeRule> GetTimeRangeRuleFromSensorSerialNumber(int serialNumber)
        {
            return Controller.DBFacades.RuleSetDBFacade.GetTimeRangeRuleFromSensorSerialNumber(serialNumber);
        }
    }
}
