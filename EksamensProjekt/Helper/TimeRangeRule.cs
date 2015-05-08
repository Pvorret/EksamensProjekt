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
        public Time TimeRange { get; set; }

        public TimeRangeRule(string cpr, string actingRule, Time timeRange) 
        {
            this.ActingRule = actingRule;
            this.CPRNR = cpr;
            this.TimeRange = timeRange;
        }
        public void GetTimeRangeRuleFromSensorSerialNumber(int serialNumber)
        {

        }
    }
}
