using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model {
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
